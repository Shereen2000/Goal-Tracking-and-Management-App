using Goal_Management_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Goal_Management_Web_App.Controllers
{
    public class HomeController : Controller
    {
         public IActionResult Index()
         {
            return View();
         }

    }
}
