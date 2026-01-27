using AspUrlShortnerer.Domain.entities;
namespace AspUrlShortnerer.Services

{
    public class Application
    {
        Application()
        {  
        }


        public ShortenUrl CreateShortenUrl()
        {
            
            //getting users link
            string inUrl;
            GetOrigLink(out inUrl);
            string code, shortUrl;
            GenShortUrl(inUrl, out code, out shortUrl);

            return (new ShortenUrl(shortUrl, inUrl, code, DateTime.UtcNow));
        }


        //getting users link
        private static string GetOrigLink(out string inUrl)
        {
            inUrl = UserInput.InputUrl();
            Validator.IsUrlValid(inUrl);
            return inUrl;
        }
        //generating random unique code
        private static void GenShortUrl(string inUrl, out string code, out string shortUrl)
        {
            UrlGen generator = new UrlGen(inUrl);
            code = generator.GenerateUniqueCode();
            shortUrl = generator.GenerateShortUrl(code);
        }

    }
}
