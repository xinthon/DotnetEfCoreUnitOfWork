using DotnetEfCoreUnitOfWork.Infrastructure.Persistence;
using DotnetEfCoreUnitOfWork.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetEfCoreUnitOfWork.Controllers;

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

    // Get all products
    [HttpGet(Name = "GetProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // Get a product by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var product = await _productService
            .GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // Create a new product
    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // Update an existing product
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] Product product)
    {
        if (product == null || product.Id != id)
        {
            return BadRequest();
        }

        var existingProduct = await _productService.GetAllProductsAsync();
        var selectedProduct = existingProduct.FirstOrDefault(p => p.Id == id);

        if (selectedProduct == null)
        {
            return NotFound();
        }

        await _productService.UpdateProductAsync(product);
        return NoContent();
    }

    // Delete a product
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var existingProduct = await _productService.GetAllProductsAsync();
        var selectedProduct = existingProduct.FirstOrDefault(p => p.Id == id);

        if (selectedProduct == null)
        {
            return NotFound();
        }

        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}

