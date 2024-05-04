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
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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
app.MapGet("/", () => "Hello world");

// Configure the HTTP request pipeline
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseCors();
app.UseMiddleware<ValidateMiddleware>();
app.UseAuthorization();
app.MapControllers();

#region Minimal UserAPI

var userApiGroup = app.MapGroup("/api/user").RequireAuthorization();

userApiGroup.MapGet("GetLoginStatus", (HttpContext HttpContext) =>
{
    return HttpContext.User.Identity is not null && HttpContext.User.Identity.IsAuthenticated;
}).AllowAnonymous();

userApiGroup.MapGet("GetUser", (AppointmentDbContext _dbContext, HttpContext HttpContext, IUserService _service) =>
{
    if (HttpContext.User.Identity is null || !HttpContext.User.Identity.IsAuthenticated)
    {
        return Results.Unauthorized();
    }

    var userID = _service.GetUserID();
    if (!userID.IsNullOrEmpty())
    {
        var user = _dbContext.UserTables.Where(x => x.UserId.ToString() == userID).FirstOrDefault();
        return Results.Ok(user);
    }
    else
    {
        return Results.BadRequest("User is not found");
    }
});

userApiGroup.MapPost("PostUser", async (AppointmentDbContext _dbContext, HttpContext HttpContext, [FromBody] UserTable user) =>
{
    if (user is null) return Results.NotFound("Requested body is null");

    try
    {
        var lastUserID = _dbContext.UserTables.OrderBy(x => x.UserId).Last().UserId;
        user.UserPassword = UserService.HashPassword(user.UserPassword);
        user.UserId = lastUserID + 1;

        _dbContext.UserTables.Add(user);
        var result = await _dbContext.SaveChangesAsync();
        return result == 1 ? Results.Ok() : Results.BadRequest("There is some problem while creating user");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

userApiGroup.MapDelete("Remove", async (AppointmentDbContext _dbContext, IUserService _service, [FromBody] UserTable user) =>
{
    var userID = _service.GetUserID();
    if (userID.IsNullOrEmpty()) return Results.NotFound("User not found");

    try
    {
        var currentUser = _dbContext.UserTables.Where(x => x.UserId.ToString() == userID).FirstOrDefault();
        if (currentUser is null) return Results.NotFound("User is not found for the mentioned id");

        _dbContext.UserTables.Remove(currentUser);
        var result = await _dbContext.SaveChangesAsync();
        return result == 1 ? Results.Ok("User deleted successfully") : Results.BadRequest("There is some problem while creating user");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

userApiGroup.MapPut("Update", async (AppointmentDbContext _dbContext, [FromBody] UserTable user) =>
{
    try
    {
        var currentUser = _dbContext.UserTables.Where(x => x.UserId == user.UserId).FirstOrDefault();
        if (currentUser is null) return Results.NotFound("User is not found for the mentioned id");

        currentUser.UserName = user.UserName;
        currentUser.UserPassword = user.UserPassword;
        currentUser.UserAddress = user.UserAddress;
        currentUser.MobileNumber = user.MobileNumber;
        currentUser.Email = user.Email;

        var result = await _dbContext.SaveChangesAsync();
        return result == 1 ? Results.Ok("User updated") : Results.BadRequest("There is some problem while creating user");

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

#endregion

app.Run();
