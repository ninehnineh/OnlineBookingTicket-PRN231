using Microsoft.AspNetCore.Mvc;

namespace OnlineBookingTicket.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
