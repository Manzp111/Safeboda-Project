using SafeBoda.Core;
using Microsoft.EntityFrameworkCore;
using SafeBoda.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<ITripRepository, EfTripRepository>();

// Correct DbContext configuration
builder.Services.AddDbContext<SafeBodaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SafeBodaDb")));

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Optional for HTTP testing
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();