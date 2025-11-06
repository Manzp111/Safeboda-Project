using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeBoda.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<SafeBodaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SafeBodaDb")));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SafeBodaDbContext>()
    .AddDefaultTokenProviders();

// Add trip repository
builder.Services.AddScoped<ITripRepositoryDb, EfTripRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();// repository of users


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
