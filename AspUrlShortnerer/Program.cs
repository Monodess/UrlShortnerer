
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
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            WebApplication app = builder.Build(); 
            if(app.Environment.IsDevelopment())
            {
                app.MapOpenApi(); 
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run(); 
            
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
