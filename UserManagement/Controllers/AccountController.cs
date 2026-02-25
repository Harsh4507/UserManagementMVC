using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Models.Request;
using UserManagement.Models.Response;

namespace UserManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return View("Index");
            var response = await _httpClient.PostAsJsonAsync("/api/Account/Login", request);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View("Index");
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            TempData["Username"] = request.Username;

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var username = TempData["Username"]?.ToString();
            ViewBag.Username = username;
            return View();
        }
    }
}