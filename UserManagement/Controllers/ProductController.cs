using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
        }
        public async Task<IActionResult> GetProducts(int categoryId)
        {
            var response = await _httpClient.GetAsync("ProductCategory/Category/{categoryId}/GetProduct");

            if (response.IsSuccessStatusCode)
            {
                var products = await response
                    .Content
                    .ReadFromJsonAsync<List<Product>>();

                return View(products);
            }


            return View(new List<Product>());
        }
    }
}
