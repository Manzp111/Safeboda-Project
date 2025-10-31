using Microsoft.EntityFrameworkCore;
using SafeBoda.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<SafeBodaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SafeBodaDb")));

// Add repository
builder.Services.AddScoped<ITripRepositoryDb, EfTripRepository>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
