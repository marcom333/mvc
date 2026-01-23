using Microsoft.AspNetCore.Mvc;
using Application.Entities;

namespace Web.Controllers;
public class ProductController : Controller
{
    
    public List<Product> products = [];

    public ProductController()
    {
        for(int i = 0;i<10;i++)
        {
            products.Add(new Product
            {
                Name="Product #"+i
            });
        }
    }
    
    
    public IActionResult Index()
    {
        return View(products);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        //*Se crea registro en BD*
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Read(int product_id)
    {
        return View(products[product_id]);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LoadUpdate(int product_id)
    {
        return View("Update",products[product_id]);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int product_id)
    {
        products.RemoveAt(product_id);
        return View(products);
    }

    public IActionResult Detail(int product_id)
    {
        if(product_id == 50)
        {
            TempData["error"] = true;
            return RedirectToAction("Index");
        }
        if(product_id==100)
        {
            return NotFound();
        }
        if(product_id == 5)
        {
            ViewBag.Warning = "Casi se terminan!!";
        }
        return View(products[product_id]);
    }

    public ViewResult ViewResult()
    {
        return View();
    }

    public JsonResult JsonResult()
    {
        return Json(new
        {
            Name = "Hello",
            Date=DateTime.Now
        });
    }

    public RedirectResult RedirectResult()
    {
        return Redirect("http://www.google.com");
    }

    public RedirectToActionResult RedirectToActionResult()
    {
        return RedirectToAction("Index","Home",new {Id = 1});
    }

    public ContentResult ContentResult()
    {
        return Content("Hello world!");
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

