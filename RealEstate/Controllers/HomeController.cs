using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

  
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new  ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
