
using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using AspUrlShortnerer.View;
namespace AspUrlShortnerer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            DAL dal = new DAL("Server=localhost;Port=3306;Database=practice_platform;Uid=root;Pwd=savepass;SslMode=Disabled;");
            Console.WriteLine(dal.Connect());
  
                try
                {
                    Uri url = UserInput.InputUrl();

               
                    using var client = new HttpClient();
                client.BaseAddress = url; 

                    //validating url 
                    var request = new HttpRequestMessage(HttpMethod.Head, url); 
                    var responce = await client.SendAsync(request);
                    Console.WriteLine(responce.StatusCode);     

                  
   
                }
                catch {
                    Console.WriteLine("Something went wrong");
                return; 
                }
        }
    }
}

            //var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.

            //builder.Services.AddControllers();
            //// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            //app.MapControllers();

            //app.Run();