using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Steeltoe.Extensions.Configuration;
using Wms.Api.Configs;
using Wms.Infrastructure;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Models.AuthModels;
using Wms.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureAppSettings(args);
//.ConfigureServiceDiscovery();

// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

// Add Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<WmsDbContext>()
    .AddDefaultTokenProviders();




builder.Services.AddCors();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wms API",
        Version = "v1",
    });
    c.DocumentFilter<DatabaseHostDocumentFilter>();
    c.OperationFilter<AddAreaToOperationFilter>();

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "Wms.Api.xml");
    c.IncludeXmlComments(filePath);
});


//JWT Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});
builder.Services.AddTransient<IStartupFilter, EnsureRolesExistFilter>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(x =>
   {
       x.RequireHttpsMetadata = false;
       x.SaveToken = true;
       x.TokenValidationParameters = new TokenValidationParameters
       {
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
           ValidateIssuerSigningKey = true,
           ValidateIssuer = false,
           ValidateAudience = false
       };
   });


var app = builder.Build();

// Applying Migrations automatically in Dev environemnt
if (app.Environment.IsDevelopment())
{
    using var serviceScope = app.Services.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<WmsDbContext>();

    // Apply pending migrations
    dbContext.Database.Migrate();

    // Seed the database IServiceProvider serviceProvider
    await DBSeed.SeedAsync(dbContext, serviceScope);
}



app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.EnableTryItOutByDefault();
    options.DocumentTitle = "Wms.Api";

});

// Configure CORS for localhost. Must change this before going to production.
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public interface IIdentityApiMarker { }


