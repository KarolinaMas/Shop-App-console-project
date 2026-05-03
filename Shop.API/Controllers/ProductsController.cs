using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.Services;

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

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return productService.GetList(1, 4);
        }
    }
}
