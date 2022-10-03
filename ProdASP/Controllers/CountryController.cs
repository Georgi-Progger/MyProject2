using Microsoft.AspNetCore.Mvc;

namespace ProdASP.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
