using Mapster;
using Microsoft.AspNetCore.Authentication.Cookies;
using PaySpace.Calculator.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMapster();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(p => {
        p.LoginPath = "/Home/Index";
    });

builder.Services.AddControllersWithViews();
builder.Services.AddCalculatorHttpServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions{
    MinimumSameSitePolicy = SameSiteMode.Lax
});
app.UseSession();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();