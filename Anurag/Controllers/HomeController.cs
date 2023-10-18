using Anurag.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Anurag.Controllers
{
    public class HomeController : Controller
    {
        List<User> obj = new List<User>();
        private readonly ILogger<HomeController> _logger;
        private new readonly IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index() => View();

        public IActionResult Privacy(User user)
        {
            if (user.Username != null && user.Password != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_config["AnuragAPI:url"]!);
                    var result = client.PostAsJsonAsync("api/AddUser", user).Result;
                    if (result.IsSuccessStatusCode)
                        if (result.Content.ReadAsStringAsync().Result != "") obj.AddRange(JsonConvert.DeserializeObject<List<User>>(result.Content.ReadAsStringAsync().Result)!);
                    else
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(obj);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}