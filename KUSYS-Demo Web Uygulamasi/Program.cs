using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using KUSYS_Demo_Web_Uygulamasi.Models;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using KUSYS_Demo_Web_Uygulamasi.Repository;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo_Web_Uygulamasi.Services.IServices;
using KUSYS_Demo_Web_Uygulamasi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CourseDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddScoped<IRepositoryCourse, RepositoryCourse>();
builder.Services.AddScoped<IRepositoryAppUsers, RepositoryAppUsers>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITokenHandler, KUSYS_Demo_Web_Uygulamasi.Services.TokenHandler>();
builder.Services.AddSwaggerDocument();

    builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    }).AddEntityFrameworkStores<CourseDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseOpenApi();
app.UseSwaggerUi();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
