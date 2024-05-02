using Microsoft.AspNetCore.Authentication.Cookies;
using QuestionnaireApp.Controllers;
using QuestionnaireApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add MongoDB connection
builder.Services.AddSingleton<DatabaseConnection>();
builder.Services.AddSingleton<Register>();
builder.Services.AddSingleton<SurveyManipulator>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Home";
        options.LogoutPath = "/";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Cookie.Name = "keksi";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAdminAccess", policy =>
    {
        policy.RequireAssertion(context =>
        (context.User.Identity.IsAuthenticated) && context.User.IsInRole("admin"));
    });
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
