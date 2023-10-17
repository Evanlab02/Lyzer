using dotenv.net;
using dotenv.net.Utilities;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;


namespace Lyzer_BE.App_Start
{
    [ExcludeFromCodeCoverage]
    public class App
    {
        public void Start(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                DotEnv.Load();
                string connectionUri = EnvReader.GetStringValue("MONGODB_CONNECTION");
                Environment.SetEnvironmentVariable("MONGODB_CONNECTION", connectionUri);
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Commented out in case we want to ever use this another time.
            //if (app.Environment.IsProduction())
            //{
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            void ConfigureServices(WebApplicationBuilder builder)
            {
                builder.Services.AddScoped<IDriverService, DriverService>();
                builder.Services.AddScoped<IScheduleService, ScheduleService>();
            }
        }
    }
}