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


        // Retrieve original URL
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> GetOriginalUrl(string shortCode)
        {
            var url = await _urlService.GetOriginalUrlAsync(shortCode);
            if (url == null)
                return NotFound(new { message = "Short URL not found" });

            return Ok(url);
        }

        // Update an existing short URL
        [HttpPut("update/{existingShortCode}")]
        public async Task<IActionResult> UpdateShortUrl(string existingShortCode, [FromBody] dynamic request)
        {
            if (string.IsNullOrWhiteSpace(request?.NewShortCode))
                return BadRequest(new { ErrorMessage = "New short code is required" });

            // Ensure that the new short code meets the desired length requirement
            if (request.NewShortCode.Length != 6)
                return BadRequest(new { ErrorMessage = "Short code must contain exactly 6 characters" });

            var updated = await _urlService.UpdateShortUrlAsync(existingShortCode, request.NewShortCode);

            if (!updated)
                return NotFound(new { ErrorMessage = "Existing short code not found" });

            return Ok(new { message = "Short code updated successfully" });
        }




        // Delete an existing short URL
        [HttpDelete("delete/{shortCode}")]
        public async Task<IActionResult> DeleteShortUrl(string shortCode)
        {
            var deleted = await _urlService.DeleteShortUrlAsync(shortCode);
            if (!deleted)
                return NotFound(new { message = "Short URL not found" });

            return NoContent();
        }

    }
}
