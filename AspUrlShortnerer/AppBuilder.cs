using AspUrlShortnerer.Application;
using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Domain.Services;
using AspUrlShortnerer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mysqlx.Session;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;
using System.Text;

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
            //      MapAPI scans code? 
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<JwtService>(); 
            builder.Services.AddScoped<InApplication>();
            builder.Services.AddScoped<DBcontextApplication>();
            builder.Services.AddScoped<ResponceApplication>();
           
            builder.Services.AddCors(options => { options
                .AddDefaultPolicy(policy => { policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod(); }); });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["UserLogin:Issuer"],
                        ValidAudience = builder.Configuration["UserLogin:audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["UserLogin:Key"]))
                    }; 
                }); 

            app = builder.Build();
            app.UseCors(); 


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            //The factory (creation)
            //These methods are used by api user 
            //if i want to send something on a server i use post 

            //ngrok config
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("ngrok-skip-browser-warning", "true");
                await next(); 
            });

            //autorization
            app.MapPost("/logic", async (UserLogin user, JwtService jwtservice) =>
            {
                if (user.Name == "admin" && user.Password == "savepass")
                {
                    var token = jwtservice.CreateToken(user);
                    //what func returns server accepts 
                    return Results.Ok(new { token });
                }
                return Results.Unauthorized();
            }
            ).AllowAnonymous(); 

            app.MapPost("/shorten", async (ShortenUrlRequest input, InApplication request, DBcontextApplication DBcontext, ResponceApplication responce) =>
            {
                //Get users link and create unique url for it

                string longurl = input.OrigUrl;
                request.GenShortUrl(longurl, out string code, out string shortUrl);

                ShortenUrl url = new ShortenUrl(shortUrl, longurl, code, DateTime.UtcNow);              //Check if insert is correct and send responce 
                if (DBcontext.InsertShortenUrl(url))
                {
                    //if SELECT field is correct send responce 
                    if (responce.SendCode(url) != null)
                    {
                        Console.WriteLine(url.ShortUrl);
                        return Results.Created($"/code/{url.Code}", new { ShortUrl = url.ShortUrl });
                    }
                }
                return Results.Problem();
            });
            //The Redirector 
            app.MapGet("/{code}", async (string code, ResponceApplication responce) =>
            {

                var redirurl = responce.Redir(code);
                if (redirurl != null)
                    return Results.Redirect(redirurl);

                else return Results.NotFound();
            });
            app.Run();
        }
    }
}
