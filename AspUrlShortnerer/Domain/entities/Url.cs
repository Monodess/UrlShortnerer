using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EF = Microsoft.EntityFrameworkCore.Design;
namespace AspUrlShortnerer.Domain.entities
   
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ShortenUrl> ShortenUrls => Set<ShortenUrl>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenUrl>()
                .Property(x => x.Id)
                
                .ValueGeneratedOnAdd();
        }
    }
    public class AppDbContextFactory() : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var cs = "Server=localhost;Port=3306;Database=practice_platform;Uid=root;Pwd=savepass;SslMode=Disabled;";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(cs, ServerVersion.AutoDetect(cs))
                .Options;

            return new AppDbContext(options);
        }
       

    }
    public class Url
    {
        
        public Uri? _url { get; set; }

        public Url() { }
        public Url(Uri? url)
        {
            _url = url;
        }
    }
    public class ShortenUrl
    {
        public ShortenUrl() { }
        public ShortenUrl(int id, string shortUrl, string longUrl, string code, DateTime createdOnUtc)
        {
            Id = id;
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

    }
       

    }
