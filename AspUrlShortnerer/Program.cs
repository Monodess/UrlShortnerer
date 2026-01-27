
using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace AspUrlShortnerer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            DAL dal = new DAL();
            dal.DisplayVal(dal.GetField(1)); 
            
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