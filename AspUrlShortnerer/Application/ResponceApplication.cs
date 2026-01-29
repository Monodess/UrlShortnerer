using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;

namespace AspUrlShortnerer.Application
{
    public class ResponceApplication
    {
        public ResponceApplication()
        {
        }

        public string SendCode(ShortenUrl url)
        {
            if (DAL.DoesCodeExist(url.Code))
            {
                return url.ShortUrl;
            }
            else return null; 
        }

        public string Redir(string code)
        {
           return DBcontextApplication.GetOrigLink(code);
        }
    }
}
