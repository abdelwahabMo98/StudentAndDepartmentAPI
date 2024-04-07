
using Lab1Sol.Entity;
using Microsoft.EntityFrameworkCore;

namespace Lab1Sol
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<SchoolContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("con"));
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyCors",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyCors");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
