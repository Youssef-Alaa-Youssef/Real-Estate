using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Account()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn()
        {
            return Content("SignIn");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp()
        {
            return Content("SignUp");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult forgotPassword()
        {
            return Content("forgotPassword");
        }

    }
}
