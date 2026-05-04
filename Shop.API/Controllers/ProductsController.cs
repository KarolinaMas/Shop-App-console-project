using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public ActionResult<List<Product>> GetAll(int page, int itemsPerPage)
        {
            return productService.GetList(page, itemsPerPage);
        }

        [HttpGet("{id}")] // galima ir ne viena parametra prideti, pvz.,  HttpGet("{id}/{priceFrom}")
        public ActionResult<Product> Get(int id)
        {
            return productService.Get(id);
        }
    }
}
