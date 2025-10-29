<<<<<<< HEAD
using SafeBoda.Core;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using SafeBoda.Infrastructure;
=======
=======


>>>>>>> ec50d1c (adding web api with controller)
using SafeBoda.Application;
>>>>>>> 91ae9ac (adding get method)

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddScoped<ITripRepository, InMemoryTripRepository>();
builder.Services.AddSingleton<ITripRepository, InMemoryTripRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

<<<<<<< HEAD
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
=======
// Add services to the container.





>>>>>>> ec50d1c (adding web api with controller)

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
<<<<<<< HEAD
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
=======
    app.UseSwagger();
    app.UseSwaggerUI();
>>>>>>> ec50d1c (adding web api with controller)
}
// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.MapControllers();

<<<<<<< HEAD
>>>>>>> 91ae9ac (adding get method)
=======
//running product
>>>>>>> ec50d1c (adding web api with controller)
app.Run();