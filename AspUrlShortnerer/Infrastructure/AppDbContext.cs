using AspUrlShortnerer.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AspUrlShortnerer.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ShortenUrl> ShortenUrls => Set<ShortenUrl>();
        public DbSet<User_ShortenUrls> UserLogins => Set<User_ShortenUrls>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenUrl>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ShortenUrl>()
                .Property(y => y.Code)
                .HasColumnType("VARCHAR(6)");
            modelBuilder.Entity<ShortenUrl>()
                .Property(s => s.ShortUrl)
                .HasColumnType("VARCHAR(29)"); //https://localhost:5020/unique 
            modelBuilder.Entity<ShortenUrl>()
                .Property(l => l.LongUrl)
                .HasColumnType("VARCHAR(2048)");

            modelBuilder.Entity<User_ShortenUrls>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User_ShortenUrls>()
                .Property(x => x.Password)
                .HasColumnType("VARCHAR(40)");
            modelBuilder.Entity<User_ShortenUrls>()
                .Property(x => x.Name)
                .HasColumnType("VARCHAR(20)");

        }
    }
}
