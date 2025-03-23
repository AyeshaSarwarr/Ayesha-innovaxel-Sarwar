namespace UrlShorteningService.Models
{
    public class UrlEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UrlVisitedCount { get; set; } = 0;
    }
}
