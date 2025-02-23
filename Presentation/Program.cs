using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;


JsonSerializerOptions options = new()
{
    WriteIndented = true,
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};


var services = new ServiceCollection();


services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");
});


services.AddScoped<CustomerRepository>();
services.AddScoped<ProductRepository>();
services.AddScoped<IProjectRepository, ProjectRepository>(); // Bind interfacet till den konkreta implementationen
services.AddScoped<CustomerService>();
services.AddScoped<ProjectService>();
services.AddScoped<MenuDialogs>();


var serviceProvider = services.BuildServiceProvider();


var menuDialogs = serviceProvider.GetRequiredService<MenuDialogs>();
await menuDialogs.ShowMenuAsync();

Console.ReadKey();
