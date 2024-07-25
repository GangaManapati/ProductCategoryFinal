using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Repository;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[Action]")]  
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;    // for dependency injection

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]                               //<List<Categories ani kudaa vadochu but edi flexible
        public async Task<ActionResult<List<Category>>> GetCategories(int pageNumber=1, int pageSize=1111)
        {
            var categories = await _categoryRepository.GetCategoriesAsync(pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetActiveCategories(int pageNumber=1, int pageSize=1111)
        {
            var categories = await _categoryRepository.GetActiveCategoriesAsync(pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetDeactivatedCategories(int pageNumber=1, int pageSize=1111)
        {
            var categories = await _categoryRepository.GetDeactivatedCategoriesAsync(pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { message = "Please select the correct ID." });
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            
            await _categoryRepository.CreateCategoryAsync(category);
            return Ok("Product created");
           // return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(id,category);
            return Ok(new { message = "category updated successfully", categoryId = id });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
             return Ok(new { message = "category Deleted successfully", categoryId = id });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> ActivateCategory(int id)
        {
            await _categoryRepository.ActivateCategoryAsync(id);
            return Ok(new { message = " category Activated  successfully", categoryId = id });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> DeactivateCategory(int id)
        {
            await _categoryRepository.DeactivateCategoryAsync(id);
            return Ok(new { message = " category DeActivated  successfully", categoryId = id });
        }
    }
}
