using Microsoft.AspNetCore.Mvc;

namespace CORE3.UI.MVC.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Status(int id)
        {
            (int id, string message) error;
            
            error.id = id;
            error.message = id switch
            {
                404 => "Page Not Found",
                400 => "Bad Request",
                500 => "Internal Server Error",
                505 => "Access Denied",
                _ => "Unknown Error"
            }; 

            return View(error);
        }
    }
}
