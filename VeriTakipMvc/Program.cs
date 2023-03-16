using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using VeriTakipMvc.Data;
using VeriTakipMvc.Data.Interfaces;
using VeriTakipMvc.Data.Repository;
using VeriTakipMvc.Models;
using VeriTakipMvc.Services;
using VeriTakipMvc.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes("elitelektronik.net");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient(typeof(ILoginRepository), typeof(LoginRepository));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddDbContext<Context>();
builder.Services.AddRazorPages()
           .AddRazorRuntimeCompilation();
builder.Services.AddTransient(typeof(Context), typeof(Context));
builder.Services.AddTransient(typeof(IDeviceRepository), typeof(DeviceRepository));
builder.Services.AddTransient(typeof(ICompanyRepository), typeof(CompanyRepository));
builder.Services.AddTransient(typeof(IJwtService), typeof(JwtService));

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
})
   .AddEntityFrameworkStores<Context>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
//builder.Services.AddMvc(config =>
//{
//    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    //The authentication in the system is set to 100 min.
    x.ExpireTimeSpan = TimeSpan.FromMinutes(100);
    //Page without permission
    x.AccessDeniedPath = new PathString("/User/PageDenied/");
    //Project start landing page
    x.LoginPath = "/User/Index/";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
