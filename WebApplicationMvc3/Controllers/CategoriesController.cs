using Microsoft.AspNetCore.Mvc;
using WebApplicationMvc3.Models;

namespace WebApplicationMvc3.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:44341/api");
        private readonly HttpClient _client;
       
        public CategoriesController()
        {

            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            
        }


        [HttpGet]
        public async Task<ActionResult> GetAllCategories(int pageNumber = 1, int pageSize = 1111)
        {
            // Call API endpoint to get categories
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Category/GetCategories");

            // Check if API call was successful
            if (response.IsSuccessStatusCode)
            {
                // Read response content as string
                string apiResponse = await response.Content.ReadAsStringAsync().Result;

                // Deserialize JSON response to list of Category objects
                var categories = JsonCon.DeserializeObject<List<Category>>(apiResponse);

                return View(categories); // Return view with categories
            }
            else
            {
                var categories = await _categoryRepository.GetCategoriesAsync(pageNumber, pageSize);
                return View(categories);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
