using Application.Interface.Repositories;
using Application.Interface.Service;
using Application.Services;
using DotNetEnv;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Web.Filters;
using Web.Tools;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => {
    var policy = new AuthorizationPolicyBuilder().
                    RequireAuthenticatedUser().
                    Build();
    
    options.Conventions.Add(new AuthorizeControllerModelConvention("Web.Controllers.Web", policy));

    var policyApi = new AuthorizationPolicyBuilder().
                    RequireAuthenticatedUser().
                    AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).
                    Build();
    options.Conventions.Add(new AuthorizeControllerModelConvention("Web.Controllers.Api", policyApi));
});

// builder.Services.AddTransient<IOutput, Output>(); // <== nuevo cada que se usa
// builder.Services.AddScoped<IOutput, Output>(); // <== duradero
builder.Services.AddSingleton<IOutput, OutputFecha>(); //<== cuidado
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<RecordSnapshotRepo>();

builder.Services.AddTransient<IAuthorizationHandler, IsAdminHandler>();

builder.Services.AddScoped<ActionFilter>();
builder.Services.AddScoped<ResultFilter>();

builder.Services.AddAuthorization(options => {
    options.AddPolicy(IsAdminRequirement.PolicyName, policy => {
        policy.Requirements.Add(new IsAdminRequirement());
    });
});

builder.Services.AddResponseCaching();

var obj = builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
obj.AddCookie(options => {
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(31);
    // options.SlidingExpiration = true;
});
obj.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["API_SECRET"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseResponseCaching();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
