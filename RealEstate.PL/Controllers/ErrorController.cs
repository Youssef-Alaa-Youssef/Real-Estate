using Microsoft.AspNetCore.Mvc;
using RealEstate.DAL.Models.Errors;

namespace RealEstate.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            var model = new ErrorViewModel
            {

                RequestId = HttpContext.TraceIdentifier
            };

            switch (statusCode)
            {
                case 404:
                    return View("Error404", model);
                case 500:
                    return View("Error500", model);
                default:
                    return View("Error", model);
            }
        }
    }
}
