using AspUrlShortnerer.Application;
using AspUrlShortnerer.Domain.entities;
using System.Security.Policy;

namespace AspUrlShortnerer
{
    public class AppBuilder
    {
        public WebApplication app; 
        
       public void Build ()
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            app = builder.Build();

            app.MapPost("shorten", async (InApplication request, ToDBApplication DBcontext, ResponceApplication responce) => {
                //Get users link and create unique url for it
                var url = request.CreateShortenUrl();
                //Check if insert is correct and send responce 
                if (DBcontext.InsertShortenUrl(url))
                {
                    //if SELECT field is correct send responce 
                    if (responce.SendResponce(url) != null)
                    {
                        Console.WriteLine(url.ShortUrl);
                        return Results.Ok(url.ShortUrl); 
                    }
                }
                 
            });
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
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
