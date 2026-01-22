using Application.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = [];

            for(int i=0; i<10; i++)
            {
                products.Add(new() {
                    Name = "Product #" + i
                });
            }

            return View(products);
        }
    }
}
