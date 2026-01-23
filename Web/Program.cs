<<<<<<< HEAD
=======
using Application.Interface.Service;
using Application.Services;
>>>>>>> b2ca0ee01653326a14ab742385c1abfdf336faba
using Web.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddTransient<IOutput, Output>(); // <== nuevo cada que se usa
// builder.Services.AddScoped<IOutput, Output>(); // <== duradero
builder.Services.AddSingleton<IOutput, OutputFecha>(); //<== cuidado

<<<<<<< HEAD
=======
builder.Services.AddSingleton<IProductService, ProductService>();

>>>>>>> b2ca0ee01653326a14ab742385c1abfdf336faba
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


app.Run();
