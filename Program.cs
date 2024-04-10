using demo.WebApiDotNetCore8;
using demo.WebApiDotNetCore8.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
   options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pickleball Court Locations API", Version = "v1" });
   options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "demo.WebApiDotNetCore8.xml"));
   options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
   {
      In = ParameterLocation.Header,
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey
   });
   options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options => { options.User.RequireUniqueEmail = false; })
   .AddRoles<IdentityRole>()
   .AddEntityFrameworkStores<DataContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
   // Default Lockout settings.
   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
   options.Lockout.MaxFailedAccessAttempts = 5;
   options.Lockout.AllowedForNewUsers = false;
});

var app = builder.Build();


/// Seed some initial Data
/// PM> dotnet run seeddata
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
   await DataSeederStandAlone.SeedUsersAndRolesAsync(app);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<IdentityUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
