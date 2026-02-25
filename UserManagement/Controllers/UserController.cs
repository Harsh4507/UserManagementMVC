using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        public UserController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7203/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Account/Login");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<User>>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }); 
                return View(users);
            }

            return View(new List<User>());
        }
    }
}
