
using Microsoft.EntityFrameworkCore;
using NC_Record_Shop_API_Mini_Project.Data;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
using NC_Record_Shop_API_Mini_Project.Services;

namespace NC_Record_Shop_API_Mini_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
        builder.Services.AddDbContext<RecordShopDbContext>(options =>
        {
            if (builder.Environment.IsDevelopment())
                options.UseInMemoryDatabase(
                    builder.Configuration.GetConnectionString("DefaultConnection")!);
            else
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")!);
        });

            builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Return a clean ProblemDetails response for any unhandled exception.
            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<RecordShopDbContext>();
                if (!db.Albums.Any())
                {
                    db.Albums.AddRange(
                        new Album { Name = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Stock = 10, Price = 14.99m },
                        new Album { Name = "Back in Black", Artist = "AC/DC", Genre = "Rock", ReleaseYear = 1980, Stock = 5, Price = 12.99m },
                        new Album { Name = "The Dark Side of the Moon", Artist = "Pink Floyd", Genre = "Progressive Rock", ReleaseYear = 1973, Stock = 8, Price = 16.99m },
                        new Album { Name = "Rumours", Artist = "Fleetwood Mac", Genre = "Soft Rock", ReleaseYear = 1977, Stock = 6, Price = 13.99m },
                        new Album { Name = "Kind of Blue", Artist = "Miles Davis", Genre = "Jazz", ReleaseYear = 1959, Stock = 4, Price = 11.99m }
                    );
                    db.SaveChanges();
                }
            }

            app.Run();
        }
    }
}
