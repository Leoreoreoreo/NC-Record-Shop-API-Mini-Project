 using Microsoft.AspNetCore.Identity;
 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore;
 using NC_Record_Shop_API_Mini_Project.Models;
 namespace NC_Record_Shop_API_Mini_Project.Data
{
    public class RecordShopDbContext : IdentityDbContext<IdentityUser>
    {
        public  RecordShopDbContext(DbContextOptions<RecordShopDbContext> options) : base(options)
        {

        }
        public DbSet<Album> Albums {get; set;}
        public DbSet<Rating> Ratings {get; set;}

        public void Seed()
        {
            if (Albums.Any()) return;
            Albums.AddRange(
                new Album { Name = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Stock = 10, Price = 14.99m },
                new Album { Name = "Back in Black", Artist = "AC/DC", Genre = "Rock", ReleaseYear = 1980, Stock = 5, Price = 12.99m },
                new Album { Name = "The Dark Side of the Moon", Artist = "Pink Floyd", Genre = "Progressive Rock", ReleaseYear = 1973, Stock = 8, Price = 16.99m },
                new Album { Name = "Rumours", Artist = "Fleetwood Mac", Genre = "Soft Rock", ReleaseYear = 1977, Stock = 6, Price = 13.99m },
                new Album { Name = "Kind of Blue", Artist = "Miles Davis", Genre = "Jazz", ReleaseYear = 1959, Stock = 4, Price = 11.99m }
            );
            SaveChanges();
        }
    }
}
