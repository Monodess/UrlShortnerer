using AspUrlShortnerer.Application;
using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;

namespace AspUrlShortnerer
{
    public class AppBuilder
    {
        public WebApplication app;

        public void Build()
        {
            //TODO: add ui for seeing api
            //      add swager for user input 
            //      note: by adding services into the builder we performed dependency injection (DI) 
            //      AddOpenAPI creates a way for developers to interact with out service 
            //      MapAPI scans our 
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<InApplication>(); 
            builder.Services.AddScoped<DBcontextApplication>(); 
            builder.Services.AddScoped<ResponceApplication>(); 
            app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            
            //The factory (creation)
            //These methods are used by api user 
            //if i want to send something on a server i use post 
            app.MapPost("/shorten", async (InApplication request, DBcontextApplication DBcontext, ResponceApplication responce) => {
                //Get users link and create unique url for it
                var url = request.CreateShortenUrl();
                //Check if insert is correct and send responce 
                if (DBcontext.InsertShortenUrl(url))
                {
                    //if SELECT field is correct send responce 
                    if (responce.SendCode(url) != null)
                    {
                        Console.WriteLine(url.ShortUrl);
                        return Results.Created($"/code/{url.Code}", new { ShortUrl = url.ShortUrl } );
                    }
                }
                return Results.Problem();
            });
            //The Redirector 
            app.MapGet("code", async (string code, ResponceApplication responce) => {
                if (!DAL.DoesCodeExist(code)) return Results.NotFound(); 

                var redirurl = responce.Redir(code);
                if (redirurl != null)
                    return Results.Redirect(redirurl);

                else return Results.NotFound(); 
            }); 
            app.Run();
        }
    }
}
