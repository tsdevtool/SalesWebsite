using CoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
