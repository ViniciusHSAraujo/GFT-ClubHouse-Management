using GFT_ClubHouse__Management.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Administrator.Controllers {
    [Area("Administrator")]
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Login() {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login([FromForm] User user) {
            return View();
        }
        
    }
}