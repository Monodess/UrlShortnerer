    namespace AspUrlShortnerer.Domain.entities
{
    public record ShortenUrlRequest(string OrigUrl);
    public record ShortUrlResponce(string Code);
}
