using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;

namespace AspUrlShortnerer.Application
{
    public class ToDBApplication
    {
        public bool InsertShortenUrl(ShortenUrl url)
        {
            return DAL.InsertField(url); 
        }
    }
}
