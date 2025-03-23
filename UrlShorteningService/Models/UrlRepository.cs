using Microsoft.EntityFrameworkCore;

namespace UrlShorteningService.Models
{
    public class UrlRepository : UrlInterface
    {
        private readonly UrlContext _context;

        public UrlRepository(UrlContext urlContext)
        {
            _context = urlContext;
        }

        // Create a new short URL
        public async Task<UrlEntity> CreateShortUrlAsync(string originalUrl)
        {
            var shortCode = GenerateShortCode();
            var urlEntity = new UrlEntity
            {
                Url = originalUrl,
                ShortCode = shortCode,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.UrlEntities.Add(urlEntity);
            await _context.SaveChangesAsync();
            return urlEntity;
        }

        // Retrieve original URL from short URL
        public async Task<string> GetOriginalUrlAsync(string shortCode)
        {
            var urlEntity = await _context.UrlEntities.FirstOrDefaultAsync(u => u.ShortCode == shortCode);
            if (urlEntity != null)
            {
                urlEntity.UrlVisitedCount++;
                await _context.SaveChangesAsync();
                return urlEntity.Url;
            }
            return null; // Not found
        }

        // Update an existing short URL
        public async Task<bool> UpdateShortUrlAsync(string existingShortCode, string newShortCode)
        {
            // Logic to find the existing short code
            var existingUrl = await _context.UrlEntities.FirstOrDefaultAsync(u => u.ShortCode == existingShortCode);

            if (existingUrl == null)
                return false; // Existing short code not found

            // Update the short code
            existingUrl.ShortCode = newShortCode;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an existing short URL
        public async Task<bool> DeleteShortUrlAsync(string shortCode)
        {
            var urlEntity = await _context.UrlEntities.FirstOrDefaultAsync(u => u.ShortCode == shortCode);
            if (urlEntity != null)
            {
                _context.UrlEntities.Remove(urlEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Get statistics (number of times accessed)
        public async Task<int> GetUrlAccessCountAsync(string shortCode)
        {
            var urlEntity = await _context.UrlEntities.FirstOrDefaultAsync(u => u.ShortCode == shortCode);
            return urlEntity?.UrlVisitedCount ?? -1;
        }

        // Generate a random short code
        private string GenerateShortCode()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // Search short URL by original URL
        public async Task<string> SearchShortUrlAsync(string originalUrl)
        {
            var urlEntity = await _context.UrlEntities
                .FirstOrDefaultAsync(u => u.Url == originalUrl);

            return urlEntity?.ShortCode; // Return the shortcode if found, otherwise null
        }
    }
}
