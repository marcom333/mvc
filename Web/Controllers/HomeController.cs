using System.Diagnostics;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Tools;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOutput _output;
    public HomeController(ILogger<HomeController> logger, IOutput output)
    {
        Infrastructure.Class1 c = new Infrastructure.Class1();
        _logger = logger;
        _output= output;
        output.Print("hello"); 
    }

    public IActionResult Index()
    {
         _output.Print("Hola desde HomeController de Karla Heras");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
