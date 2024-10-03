using Microsoft.AspNetCore.Mvc;

namespace FlightSystemManagement.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}