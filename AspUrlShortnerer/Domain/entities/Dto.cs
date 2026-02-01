    namespace AspUrlShortnerer.Domain.entities
{
    public class UserLogin
    {
        //each field will be a added to jwt claims
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public record ShortenUrlRequest(string OrigUrl);
    public record ShortUrlResponce(string Code);
}
