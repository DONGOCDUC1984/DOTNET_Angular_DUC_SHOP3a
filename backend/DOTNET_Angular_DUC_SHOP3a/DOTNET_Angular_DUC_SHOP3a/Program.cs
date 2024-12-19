using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.Extensions.FileProviders;
using DOTNET_Angular_DUC_SHOP3a.Redis;

var builder = WebApplication.CreateBuilder(args);
// This for MYSQL
builder.Services.AddDbContext<AppDbContext>(
options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DevConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});
// End for MYSQL
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//For Redis Cache
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// To connect with Frontend like Angular
app.UseCors(options => options.WithOrigins("http://localhost:4200")
   .AllowAnyMethod()
   .AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path
    .Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
