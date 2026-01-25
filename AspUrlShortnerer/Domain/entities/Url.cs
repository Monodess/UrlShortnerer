namespace AspUrlShortnerer.Domain.entities
{
    public class Url
    {
        public Uri? _url { get; set; }

        public Url() { }
        public Url(Uri? url)
        {
            _url = url;
        }
    }
    public class ShortenUrl
    {
        public ShortenUrl(Guid id, string shortUrl, string longUrl, string code, DateTime createdOnUtc)
        {
            Id = id;
            ShortUrl = shortUrl;
            LongUrl = longUrl;
            Code = code;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOnUtc { get; set; }

    }

}
