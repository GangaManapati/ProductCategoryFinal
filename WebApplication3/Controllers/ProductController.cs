using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Repository;


namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 
       private readonly IProductRepository _productRepository;

            public ProductController(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            [HttpGet]  //  /Product/GetAll
            public async Task<ActionResult<List<Product>>> GetAll(int pageNumber = 1, int pageSize = 11111)
            {
                var products = await _productRepository.GetProductsAsync(pageNumber, pageSize);
                return Ok(products);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Product>> GetById(int id)
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found with the provided ID." });
                }
                return Ok(product);
            }

            [HttpPost]
            public async Task<ActionResult> CreateProduct(Product product)
            {
                await _productRepository.AddProductAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> UpdateProduct(int id, Product product)
            {
                if (id != product.Id)
                {
                    return BadRequest(new { message = "Product ID mismatch." });
                }

                await _productRepository.UpdateProductAsync(id, product);
                return Ok(new { message = "Product updated successfully.", productId = id });
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteProduct(int id)
            {
                await _productRepository.DeleteProductAsync(id);
                return Ok(new { message = "Product deleted successfully.", productId = id });
            }

            [HttpPost("{id}")]
            public async Task<ActionResult> ActivateProduct(int id)
            {
                await _productRepository.ActivateProductAsync(id);
                return Ok(new { message = "Product activated successfully.", productId = id });
            }

            [HttpPost("{id}")]
            public async Task<ActionResult> DeactivateProduct(int id)
            {
                await _productRepository.DeactivateProductAsync(id);
                return Ok(new { message = "Product deactivated successfully.", productId = id });
            }

            [HttpGet]
            public async Task<ActionResult<List<Product>>> GetAllActiveProducts(int pageNumber = 1, int pageSize = 11111)
            {
                var activeProducts = await _productRepository.GetAllActivateProductAsync(pageNumber, pageSize);
                return Ok(activeProducts);
            }

            [HttpGet]
            public async Task<ActionResult<List<Product>>> GetAllDeactivatedProducts(int pageNumber = 1, int pageSize = 11111)
            {
                var deactivatedProducts = await _productRepository.GetAllDeactiveProAsync(pageNumber, pageSize);
                return Ok(deactivatedProducts);
            }
        }
    }
