global using az204quizmasterAPI.Data;
global using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using az204quizmasterAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = "";
if (builder.Environment.IsDevelopment())
{
    connectionString = File.ReadAllText("localSecret.txt");
    System.Console.WriteLine("Development mode");

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowedOrigins", policy =>
        {
            policy.WithOrigins("http://localhost:8080/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
    });

}
else
{
    SecretClientOptions options = new SecretClientOptions()
    {
        Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
    };

    var client = new SecretClient(new Uri("https://az204quizmasterkeyvault.vault.azure.net/"), new DefaultAzureCredential(), options);

    System.Diagnostics.Trace.WriteLine("Logged in.");

    KeyVaultSecret secret = client.GetSecret("ConnectionString");

    connectionString = secret.Value;
}

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
});

builder.Services.AddTransient<JsonIntakeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
