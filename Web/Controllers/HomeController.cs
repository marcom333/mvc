using System.Diagnostics;
using Application.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RecordSnapshotRepo _repo;

    public HomeController(ILogger<HomeController> logger, RecordSnapshotRepo repo){
        Infrastructure.Class1 c = new Infrastructure.Class1();
        _logger = logger;
        _repo = repo;
    }

    public IActionResult Index(){
        return View();
    }

    public IActionResult Privacy(){
        return View();
    }

    public IActionResult Builder() {
        var stopwatch = Stopwatch.StartNew();
        _repo.Builder(new RecordSnapshot());
        stopwatch.Stop();
        dynamic data = new {time = stopwatch.ElapsedMilliseconds};
        Console.WriteLine($"Builder: {stopwatch.ElapsedMilliseconds} ms");
        return Ok(data);
    }
    public IActionResult NoBuilder() {
        var stopwatch = Stopwatch.StartNew();
        _repo.NoBuilder(new RecordSnapshot());
        stopwatch.Stop();
        dynamic data = new {time = stopwatch.ElapsedMilliseconds};
        Console.WriteLine($"Builder: {stopwatch.ElapsedMilliseconds} ms");
        return Ok(data);
    }
    public IActionResult Init() {
        for(int i = 0; i<5000; i++)
            _repo.Init();
        return Ok();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
