using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductOrdering.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();

var conString = "data source=SOHIT\\SQLEXPRESS;initial catalog=OrderDb;persist security info=True;Integrated Security=SSPI;TrustServerCertificate=true";
builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseSqlServer(conString);
});


builder.Services.AddSwaggerGen();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<OrderContext>();
        var pendingMigration = context.Database.GetPendingMigrations();
        if (pendingMigration.Count() > 0)
            context.Database.Migrate();
    }
}
catch (Exception ex)
{

    throw ex;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
