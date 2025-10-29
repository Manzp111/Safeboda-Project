using SafeBoda.Core;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using SafeBoda.Infrastructure;
=======
using SafeBoda.Application;
>>>>>>> 91ae9ac (adding get method)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Dependency Injection
<<<<<<< HEAD
builder.Services.AddScoped<ITripRepository, EfTripRepository>();

// Correct DbContext configuration
builder.Services.AddDbContext<SafeBodaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SafeBodaDb")));

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
=======
builder.Services.AddScoped<ITripRepository, InMemoryTripRepository>();
// builder.Services.AddSingleton<ITripRepository, InMemoryTripRepository>();


// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Standard Swagger
>>>>>>> 91ae9ac (adding get method)

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
<<<<<<< HEAD
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Optional for HTTP testing
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
=======
    app.UseSwagger();      // Swagger JSON
    app.UseSwaggerUI();    // Interactive UI at /swagger
}

// app.UseHttpsRedirection(); // Optional: can comment this for HTTP testing
app.UseAuthorization();

app.MapControllers();

>>>>>>> 91ae9ac (adding get method)
app.Run();