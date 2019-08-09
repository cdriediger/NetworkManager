using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
namespace NetworkManager
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}