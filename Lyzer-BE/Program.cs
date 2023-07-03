using dotenv.net;
using dotenv.net.Utilities;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Schedulers.Hydraters;
using System.Diagnostics.CodeAnalysis;

App program = new();
program.main(args);

[ExcludeFromCodeCoverage]
public class App
{
    public void main(string[] args)
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

        if (app.Environment.IsProduction())
        {
            var year = DateTime.Now.Year;
            IHydrationService hydrationService = HydrationService.Instance;
            hydrationService.HydrateSchedule(year.ToString());
            hydrationService.HydrateSchedule((year + 1).ToString());
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHydrationService, HydrationService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();

            //Scheduled Services
            builder.Services.AddHostedService<CurrentScheduleHydrater>();
        }
    }
}


