using Application.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    //Product
    public class ProductController : Controller
    {
        //Index
        public IActionResult Index()
        {
            List<Product> products = [];

            if (TempData["error"] != null)
                ViewBag.Error = "No existen productos en esa categoria";


            for (int i = 0; i < 10; i++)
            {
                products.Add(new()
                {
                    Name = "Product " + (i + 1)
                });
            }

            return View(products);
            //Views/Product/Index.cshtml
            //Views/Shared/
        }

        public IActionResult Details(int id)
        {
            if (id == 100)
                return NotFound();

            return View(new Product()
            {
                Name = "Product " + id,
                Description = "Description product" + id,
                Price = 1,
                CategoryId = 1,
                UserId = 1
            });
        }

        [HttpGet] //Controller/Create
        public IActionResult Create() //get por defecto, si falla poner [HttpGet]
        {
            return View(); //Product/Create.cshtml
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            //guardar en base de datos

            TempData["status"] = 200;
            return RedirectToAction("Detail");
        }

        public ViewResult ViewResult()
        {
            return View();
        }

        public JsonResult JsonResult()
        {
            return Json(new
            {
                Name = "Diana",
                Date = DateTime.Now
            });
        }

        public RedirectResult RedirectResult()
        {
            //externos
            return Redirect("https://www.google.com");
        }

        public RedirectToActionResult RedirectToActionResult()
        {
            //internos
            return RedirectToAction("Index", "Home", new { Id = 1 });
        }

        public ContentResult ContentResult()
        {
            return Content("Hola");
        }
        public NotFoundResult NotFoundResult()
        {
            return NotFound();
        }
        public OkObjectResult OkObjectResult()
        {
            return Ok(new { }); //200
        }
        public BadRequestResult BadRequestResult()
        {
            return BadRequest(); //400
        }
    }
}
