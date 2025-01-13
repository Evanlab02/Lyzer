using Lyzer.Clients;
using Lyzer.Middleware;
using Lyzer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CacheService>();

builder.Services.AddScoped<JolpicaClient>();
builder.Services.AddScoped<DriverService>();
builder.Services.AddScoped<RacesService>();
builder.Services.AddScoped<ResultsService>();
builder.Services.AddScoped<ConstructorService>();

var app = builder.Build();

Console.WriteLine($"Application running on: {string.Join(", ", builder.WebHost.GetSetting("urls"))}");

// Empty for now
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();