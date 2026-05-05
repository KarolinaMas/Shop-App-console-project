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

        [HttpGet("pages/{page}/{itemsPerPage}")]
        public async Task<ActionResult<List<Product>>> GetAllAsync(int page, int itemsPerPage)
        {
            return await productService.GetListAsync(page, itemsPerPage);
        }

        [HttpGet("{id}")] // galima ir ne viena parametra prideti, pvz.,  HttpGet("{id}/{priceFrom}")
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await productService.GetAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProduct product)
        {
            await productService.AddAsync(product);
            return Created();
        }
    }
}
