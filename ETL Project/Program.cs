using ETL_Project.Data;
using ETL_Project.Helpers;
using ETL_Project.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var configuration = new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

var csvSettings = configuration.GetSection("CsvSettings").Get<CsvSettings>()!;
string connection = configuration.GetConnectionString("DefaultConnection")!;

var serviceProvider = new ServiceCollection()
    .AddDbContext<ETLContext>(options =>
        options.UseSqlServer(connection))
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton(csvSettings)
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    .AddScoped<VendorReader>()
    .AddScoped<VendorWriter>()
    .AddScoped<VendorRepository>()
    .AddScoped<VendorImporter>()
.BuildServiceProvider();

var vendorImporter = serviceProvider.GetRequiredService<VendorImporter>();

var sw = Stopwatch.StartNew();
Console.WriteLine("Data has started importing...");

try
{
    await vendorImporter.ImportCsvDataAsync();
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Incorrect input: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unhandled exception: {ex.Message}");
}

Console.WriteLine($"Done in {Math.Round(sw.Elapsed.TotalSeconds, 2)} seconds");
