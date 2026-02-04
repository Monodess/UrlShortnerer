using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AspUrlShortnerer.Infrastructure
{
    public class AppDbContextFactory() : IDesignTimeDbContextFactory<AppDbContext>
    {
      
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var cs = config.GetConnectionString("cs"); 
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(cs, ServerVersion.AutoDetect(cs))
                .Options;

            return new AppDbContext(options);
        }
    }
}
