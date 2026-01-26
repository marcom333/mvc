using Microsoft.AspNetCore.Mvc;
using Application.Entities;
using Application.Interface.Service;
using System.Threading.Tasks;

namespace Web.Controllers;
public class ProductController : Controller
{
    
    public IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    
    public IActionResult Index()
    {
        return View(_productService.GetProducts());
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
        _productService.CreateProduct(product);
        //*Se crea registro en BD*
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Read(int product_id)
    {
        
        Product? product = _productService.GetProduct(product_id);
        if(product!= null)
        {
            return View(product);
        }
        TempData["MensajeError"] = "Ocurrió un error obteniendo los datos del producto";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LoadUpdate(int product_id)
    {
        Product? product = _productService.GetProduct(product_id);
        if(product!= null)
        {
            return View("Update",product);
        }
        TempData["MensajeError"] = "Ocurrió un error obteniendo los datos del producto";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        _productService.UpdateProduct(product);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int product_id)
    {
        _productService.DeleteProduct(new Product{ProductId = product_id});
        return RedirectToAction("Index");
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
        return View(_productService.GetProduct(product_id));
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

