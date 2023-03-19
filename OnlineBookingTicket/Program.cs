using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BusinessObject;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OnlineBookingTicketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineBookingTicketContext") ?? throw new InvalidOperationException("Connection string 'OnlineBookingTicketContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "mycookie";
    });

builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCookiePolicy();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
