using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatronesDeSeguridad.Models;
using System.Diagnostics;

namespace PatronesDeSeguridad.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Empleados()
        {
            return View();
        }
        public IActionResult Compras()
        {
            return View();
        }
        public IActionResult Ventas()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}