using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Data;
using AuthenticationAPI.Interfaces;
using AuthenticationAPI.Models;
using AuthenticationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection1"));
    //options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection2"));
});

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

// Startup.cs
//services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

//services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);

//services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = Configuration["Jwt:Issuer"],
//        ValidAudience = Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//    };
//});

//services.AddAuthorization();

//// ...
