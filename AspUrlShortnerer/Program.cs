
using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using AspUrlShortnerer.Application;
using AspUrlShortnerer.View;

namespace AspUrlShortnerer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppBuilder builder = new AppBuilder();
            builder.Build(); 
            
        }   
    }
}

           // var url = InApplication.CreateShortenUrl();
           //if( ToDBApplication.InsertShortenUrl(url))
           // {
           //     if (ResponceApplication.SendResponce(url) != null) {
           //         Console.WriteLine(url.ShortUrl);
           //         
           //     }
                
           // }
