using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EF = Microsoft.EntityFrameworkCore.Design;
namespace AspUrlShortnerer.Domain.entities
   
{
   
    
    public class ShortenUrl
    {
        public ShortenUrl() { }
        //for reading
        public ShortenUrl(int id, string shortUrl, string longUrl, string code, DateTime createdOnUtc)
        {
            Id = id;
            ShortUrl = shortUrl;
            LongUrl = longUrl;
            Code = code;
            CreatedOnUtc = createdOnUtc;
        }
        //for creating (without id)
        public ShortenUrl( string shortUrl, string longUrl, string code, DateTime createdOnUtc)
        {
            ShortUrl = shortUrl;
            LongUrl = longUrl;
            Code = code;
            CreatedOnUtc = createdOnUtc;
        }
        
        public int Id { get; set; }    
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOnUtc { get; set; }

       public override string ToString()
        {
            return $"Url #{Id}: {ShortUrl}, {LongUrl}, {Code}, {CreatedOnUtc}";
        }
    }

    }
