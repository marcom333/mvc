using Application.Interface.Repositories;
using Application.Interface.Service;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Web.Filters;
using Web.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    });

// builder.Services.AddTransient<IOutput, Output>(); // <== nuevo cada que se usa
// builder.Services.AddScoped<IOutput, Output>(); // <== duradero
builder.Services.AddSingleton<IOutput, OutputFecha>(); //<== cuidado

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddTransient<IAuthorizationHandler, IsAdminHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy(IsAdminRequirement.PolicyName, policy => {
        policy.Requirements.Add(new IsAdminRequirement());
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseStaticFiles();

app.Run();
