
using Application.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api;

[Route("Api/Category")]
[ApiController]
public class CategoryApiController: ControllerBase {
    
    public readonly IProductRepository repository;
    public CategoryApiController(IProductRepository productRepository) {
        repository = productRepository;
    }


    // Category/1/Products
    [HttpGet("{id}/Products")]
    public async Task<IActionResult> GetProducts(int id) {
        return Ok(await repository.GetAllWithPage(1, 100, "potato"));
    }

}