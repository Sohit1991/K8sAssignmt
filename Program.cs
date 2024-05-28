using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductOrdering.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductOrdering.Entity;

var builder = WebApplication.CreateBuilder(args);

string server, port, databaseName, dbUserName, dbPassword, connString = string.Empty;

server = Environment.GetEnvironmentVariable("SERVER") ?? string.Empty;
port = Environment.GetEnvironmentVariable("DB_PORT") ?? string.Empty;
databaseName = Environment.GetEnvironmentVariable("DB_NAME") ?? string.Empty;
dbUserName = Environment.GetEnvironmentVariable("DB_USERNAME") ?? string.Empty;
dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? string.Empty;
//connString = $"Server={server},{port};Database={databaseName};User Id={dbUserName};password={dbPassword};";
connString = $"Server=mssql-service.default.svc.cluster.local;Database={databaseName};User Id={dbUserName};password={dbPassword};TrustServerCertificate=true";
Console.WriteLine($"Connection string created {connString}");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();

builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseSqlServer(connString);
});


builder.Services.AddSwaggerGen();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<OrderContext>();
        Console.WriteLine("Inside migration block");
        //var pendingMigration = context.Database.GetPendingMigrations();
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Db created successfully");
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetOrders());
                await context.SaveChangesAsync();
                Console.WriteLine("Seed data successfully");
                //logger.LogInformation($"Ordering Database Seeded: {typeof(OrderContext).Name}");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine("Inside catch block of migration");
    Console.WriteLine(ex);
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();

static IEnumerable<Order> GetOrders()
{
    return new List<Order>
        {
            new()
            {
                UserName = "Sohit",
                EmailAddress = "sohit@gmail.com",
                //Id=1,
                Description="sports related product",
                Price=2000,
                ProductName="Cricket Bat"

            },
            new()
            {
                UserName = "skk",
                EmailAddress = "skk@gmail.com",
                //Id=2,
                Description="eatable products",
                Price=100,
                ProductName="Pizaa"

            },
            new()
            {
                UserName = "John",
                EmailAddress = "John@gmail.com",
                //Id=3,
                Description="Electronics product",
                Price=5000,
                ProductName="Samsung Phone"

            }
        };
}