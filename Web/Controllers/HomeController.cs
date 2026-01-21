using System.Diagnostics;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Models;
using Web.Tools;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOutput _output;
    private readonly IOutput _output2;

    public HomeController(ILogger<HomeController> logger,IOutput output,IOutput output2)
    {
        Infrastructure.Class1 c = new Infrastructure.Class1();
        _logger = logger;
        _output = output;
        _output2 = output2;
    }

    public IActionResult Index()
    {
        _output.Print("AAAAAA INYECCION");
        _output2.Print("AAAAAA INYECCION 2");
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
