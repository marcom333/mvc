using System.Reflection.Metadata.Ecma335;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Web.Controllers;

public class ProductController : Controller
{
    public IActionResult Index(){
        List<Product> produtos = [];
        for(int i =0; i <10; i++){
            produtos.Add(new(){
                Nombre="Producto #"+i
            });
        }
        return View(produtos);
        //Views/Products/Index.cshtml
    }

    public IActionResult Detail(int id)
    {
        if(id == 100)
        return NotFound();
        return View(new Product()
        {
            Nombre= "Detail prodcut" + id,
            Descripcion = "Test Product",
            Precio = 1,
            CategoryId = 1,
            UsuarioId = 1
        });
    }

    public ViewResult ViewResult()
    {
        return View();
    }

    public JsonResult JsonResult()
    {
        return Json(new
        {
            Name="Hello",
            Date = DateTime.Now
        });
    }

    public RedirectResult RedirectResult()
    {
        return Redirect("http://www.google.com");
    }

    public RedirectToActionResult RedirectToActionResult()
    {
        return RedirectToAction("Index", "Home", new {Id=1});
    }

    public ContentResult ContentResult()
    {
        return Content("Hello world");
    }

    public NotFoundResult NotFoundResult()
    {
        return NotFound();
    }

    public OkObjectResult OkObjectResult()
    {
        return Ok(new {});
    }

    public BadRequestResult BadRequestResult()
    {
        return BadRequest();
    }

}