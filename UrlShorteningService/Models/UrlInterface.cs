namespace UrlShorteningService.Models
{
    public interface UrlInterface
    {
        Task<UrlEntity> CreateShortUrlAsync(string originalUrl);

        // Retrieve the original URL from a short URL
        Task<string> GetOriginalUrlAsync(string shortCode);

        // Update an existing short URL
        Task<bool> UpdateShortUrlAsync(string existingShortCode, string shortCode);

        // Delete an existing short URL
        Task<bool> DeleteShortUrlAsync(string shortCode);

        // Get statistics on the short URL (e.g., number of times accessed)
        Task<int> GetUrlAccessCountAsync(string shortCode);
        Task<string> SearchShortUrlAsync(string originalUrl);

    }
}
