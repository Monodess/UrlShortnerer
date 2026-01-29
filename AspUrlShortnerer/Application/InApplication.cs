using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Domain.Services;
using AspUrlShortnerer.Services;
namespace AspUrlShortnerer.Application

{
    //This layer is made for use-cases
    //
    public class InApplication
    {
        public InApplication()
        {  
        }

        

        public ShortenUrl CreateShortenUrl()
        {
            
            //getting users link
            var inUrl = GetOrigLink();
            string code, shortUrl;
            GenShortUrl(inUrl, out code, out shortUrl);

            return (new ShortenUrl(shortUrl, inUrl, code, DateTime.UtcNow));
        }

        //getting users link
        private string GetOrigLink()
        {
            UserInputService request = new UserInputService(); 
            var inUrl = request.InputUrl();
            ValidatorService.IsUrlValid(inUrl);
            return inUrl;
        }
        //generating random unique code
        private static void GenShortUrl(string inUrl, out string code, out string shortUrl)
        {
            UrlGenService generator = new UrlGenService(inUrl);
            code = generator.GenerateUniqueCode();
            shortUrl = generator.GenerateShortUrl(code);
        }

    }
}
