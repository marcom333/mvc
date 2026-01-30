using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.Models;
using Web.Tools;

namespace Web.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IOutput output1, IOutput output2){
        Console.WriteLine("Constructor");

        Infrastructure.Class1 c = new Infrastructure.Class1();
        _logger = logger;
        output1.Print("Uno");
        // uuid Uno
        output2 = new Output();
        output2.Print("Dos");
        // uuid Dos
    }

    public IActionResult Index(){
        ViewData["nav"] = "home";
        return View();
    }

    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public IActionResult Privacy(){
        Console.WriteLine("Action!");
        int a = 0;
        for(int i=0; i < 10000000; i++){
            a++;
        }
        Console.WriteLine("a:"+a);
        ViewData["nav"] = "privacy";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? code){
        ViewBag.code = code;
        return View();
    }
}
