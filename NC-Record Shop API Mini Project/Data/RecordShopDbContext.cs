 using Microsoft.EntityFrameworkCore;
 using NC_Record_Shop_API_Mini_Project.Models;
 namespace NC_Record_Shop_API_Mini_Project.Data
{
    public class RecordShopDbContext : DbContext
    {
        public  RecordShopDbContext(DbContextOptions<RecordShopDbContext> options) : base(options)
        {
            
        }
        public DbSet<Album> Albums {get; set;}
    }
}