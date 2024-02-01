using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Webapp.Interfaces;
using Webapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
{
    options.Cookie.Name = "SrwasCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISrwasButikServices, SrwasButikServices>();
builder.Services.AddHttpClient<ISrwasButikServices, SrwasButikServices>();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
