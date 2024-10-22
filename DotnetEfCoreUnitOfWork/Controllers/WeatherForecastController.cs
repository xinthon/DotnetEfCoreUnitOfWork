using DotnetEfCoreUnitOfWork.Infrastructure.Persistence;
using DotnetEfCoreUnitOfWork.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEfCoreUnitOfWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productService;
        public ProductController(
            ILogger<ProductController> logger, 
            ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productService
                .GetAllProductsAsync();
        }
    }
}
