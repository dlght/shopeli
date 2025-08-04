using Microsoft.AspNetCore.Mvc;
using Shopeli.ProductManagement.API.Models;
using Shopeli.ProductManagement.API.Services;

namespace Shopeli.ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductRequest request)
        {
            try
            {
                var product = await _productService.CreateProductAsync(request);
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating product: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productService.GetProductAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving products: {ex.Message}");
            }
        }

        [HttpGet("by-date/{date:datetime}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByDate(DateTime date)
        {
            try
            {
                var products = await _productService.GetProductsByDateAsync(date);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving products by date: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(string id, UpdateProductRequest request)
        {
            try
            {
                var product = await _productService.UpdateProductAsync(id, request);
                return product == null ? NotFound() : Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating product: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            try
            {
                var success = await _productService.DeleteProductAsync(id);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting product: {ex.Message}");
            }
        }
    }
}