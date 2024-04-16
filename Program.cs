using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using DinkToPdf;
using DinkToPdf.Contracts;
using Grupp3Hattmakaren.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HatContext>(options => options.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("HatContext")));


builder.Services.AddDbContext<HatContext>(options =>
          options.UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("HatContext")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<HatContext>()
    .AddDefaultTokenProviders();

//DinkToPdf IConverter med singleton lifecycle
builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));

//ViewRenderService och PDFController (heter controller men är egentligen en service)
builder.Services.AddTransient<ViewRenderService>();
builder.Services.AddTransient<PDFController>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
