using System.Diagnostics;
using Application.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RecordSnapshotRepo _repo;

    public HomeController(ILogger<HomeController> logger, RecordSnapshotRepo repo){
        Console.WriteLine("Constructor");
        Infrastructure.Class1 c = new Infrastructure.Class1();
        _logger = logger;
        _repo = repo;
    }

    public IActionResult Index(){
        return View();
    }

    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public IActionResult Privacy(){
        Console.WriteLine("Action");
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

    [Route("Home/Error/{code?}")]
    public IActionResult Error(int ? code) {
        ViewBag.Code = code;
        return View();
    }























    public ViewResult ViewResult() {
        return View();
    }

    public JsonResult JsonResult() {
        return Json(new {
            Name="Hello",
            Date = DateTime.Now
        });
    }

    public RedirectResult RedirectResult() {
        return Redirect("http://www.google.com");
    }

    public RedirectToActionResult RedirectToActionResult() {
        return RedirectToAction("Index", "Home", new {Id=1});
    }

    public ContentResult ContentResult() {
        return Content("Hello world");
    }

    public NotFoundResult NotFoundResult() {
        return NotFound();
    }

    public OkObjectResult OkObjectResult() {
        return Ok(new {}); // 200
    }

    public BadRequestResult BadRequestResult() {
        return BadRequest(); //400
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
