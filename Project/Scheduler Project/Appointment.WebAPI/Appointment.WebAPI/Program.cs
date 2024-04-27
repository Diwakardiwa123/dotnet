using Appointment.WebAPI.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using System.Text;
using System.Text.Json.Serialization;
using Appointment.WebAPI.Middleware;
using Appointment.WebAPI.Service;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().WithMethods(["GET", "POST", "DELETE", "OPTION", "PUT", "PATCH"]).AllowAnyHeader();
    });
});
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = config["JWTSettings:Issuer"],
        ValidAudience = config["JWTSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JWTSettings:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<AppointmentDbContext>(option =>
{
    option.UseSqlServer("Data Source=.;Initial Catalog=AppointmentDB;Integrated Security=True;Trust Server Certificate=True", 
        sqlServerOptionsAction: sqlOption =>
        {
            sqlOption.EnableRetryOnFailure(6, TimeSpan.FromSeconds(5), null);            
        });
});
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();
// Configure the HTTP request pipeline
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseCors();
app.UseMiddleware<ValidateMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
