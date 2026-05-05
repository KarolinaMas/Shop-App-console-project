using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.Services;
using Shop.Services.Models;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("{page}/{itemsPerPage}")]
        public async Task<ActionResult<List<Product>>> GetAllAsync(int page, int itemsPerPage)
        {
            return await productService.GetListAsync(page, itemsPerPage);
        }

        [HttpGet("{id}")] // galima ir ne viena parametra prideti, pvz.,  HttpGet("{id}/{priceFrom}")
        public IActionResult Get(int id)
        {
            var product = productService.Get(id);

            if (product == null)
                return NotFound();

            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateProduct product)
        {
            productService.Add(product);
            return Created();
        }
    }
}
