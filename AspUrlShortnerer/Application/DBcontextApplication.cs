using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;

namespace AspUrlShortnerer.Application
{
    public class DBcontextApplication
    {
        public DBcontextApplication()
        {
        }

        public bool InsertShortenUrl(ShortenUrl url)
        {
            return DAL.InsertField(url); 
        }
        public static string GetOrigLink(string code)
        {
            return (DAL.GetByCode(code.Trim()).LongUrl.Trim());
        }
    }
}
