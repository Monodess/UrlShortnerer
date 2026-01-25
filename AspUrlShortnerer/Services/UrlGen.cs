using AspUrlShortnerer.Domain.entities; 
using System.Net;
namespace AspUrlShortnerer.Services
{
    static public class UrlGen
    {
        static public ShortenUrl GenerateShortUrl(Uri url) {
            var arr = url.Segments;
            Console.WriteLine(string.Join(",", arr));
            return new ShortenUrl(); 
        }  
    }
}
