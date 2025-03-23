using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrlShorteningService.Models;

namespace UrlShorteningService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlInterface _urlService;

        public UrlController(UrlInterface urlService)
        {
            _urlService = urlService;
        }

        // Create a short URL (directly receiving string)
        [HttpPost("create")]
        public async Task<IActionResult> CreateShortUrl([FromBody] string originalUrl)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
                return BadRequest(new { message = "Original URL is required" });

            var result = await _urlService.CreateShortUrlAsync(originalUrl);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Failed to create short URL" });

            return CreatedAtAction(nameof(GetOriginalUrl), new { shortCode = result.ShortCode }, new
            {
                originalUrl = result?.Url ?? originalUrl,  
                shortUrl = $"{result.ShortCode}" 
            });
        }


 }