
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
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();
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
                db.Seed();
            }

            app.Run();
        }
    }
}
