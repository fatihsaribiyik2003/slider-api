using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SubuProtokol.Core.Enums;
using SubuProtokol.Services.EntityFramework.Abstract;
using SubuProtokol.WEBUI.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IApiServiceClient, ApiServiceClient>();
builder.Services.AddScoped<IApiService, ApiService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    {
        //?

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "issuer",
            ValidAudience = "audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecurityKey")),
            ClockSkew = TimeSpan.Zero


        };
    });

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromHours(24);
    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.HttpOnly = true;
});
builder.Services.AddHttpContextAccessor();

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
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/Dashboard") || context.Request.Path.StartsWithSegments("/ProtokolAdmin")
|| context.Request.Path.StartsWithSegments("/User") || context.Request.Path == "/Protokol/ProtokolListAll", config =>
{
    config.Use(async (context, next) =>
    {
        var UserName = context.Request.Cookies["username"];
        var Role = context.Request.Cookies["role"];

        // Kullanýcýnýn cookie bilgileri boþsa login sayfasýna yönlendirir.
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Role))
        {
            context.Response.Redirect("/Auth/Login");
            return;
        }

        if (Role != "4" && Role != "3") //Admin veya Moderatör deðilse
        {
            context.Response.Redirect("/Protokol/ProtokolListAll");
            return;
        }


        // Kullanýcýnýn session bilgileri varsa context Items' a deðerleri doldurup devam edilir.
        context.Items["UserName"] = UserName;
        context.Items["Role"] = Role.To<int>().ToEnum<EnumUserRole>().GetDescription();

        await next.Invoke();
    });
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(

    name: "default",
    pattern: "{controller=Home}/{action=LoginScreen}/{id?}");
app.MapControllerRoute(

    name: "areaRoute",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
