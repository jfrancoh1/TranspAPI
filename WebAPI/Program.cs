using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.DbContextEntity;
using Mappings;
using Application.Interface;
using Application.Main;
using Infrastructure.Interface;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add Cors
var rulesCors = "RulesCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: rulesCors, builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add JWT

builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("Jwt").GetSection("secretkey").ToString();
var issuer = builder.Configuration.GetSection("Jwt:Issuer").Value;
var audience = builder.Configuration.GetSection("Jwt:Audience").Value;
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(config =>
{

    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true

    };
});
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IUsers, Users>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBranches, Branches>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IAuthentication, Authentication>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 2,
        maxRetryDelay: TimeSpan.FromSeconds(50),
        errorNumbersToAdd: null);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();



app.Run();
