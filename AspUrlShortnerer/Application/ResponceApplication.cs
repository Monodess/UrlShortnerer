using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;

namespace AspUrlShortnerer.Application
{
    public class ResponceApplication
    {
        public string SendResponce(ShortenUrl url)
        {
            if (DAL.DoesCodeExist(url.Code))
            {
                return url.ShortUrl;
            }
            else return null; 
        }

        
    }
}
