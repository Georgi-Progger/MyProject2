using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdASP.Data;
using ProdASP.Models;
using System.Diagnostics;

namespace ProdASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _db;
        private IWebHostEnvironment _appEnvironment;
        public HomeController(ILogger<HomeController> logger, ApplicationContext db, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _db = db;
            _appEnvironment = appEnvironment;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Index()
        {
            var model = new AllViewPlace
            {
                Country = _db.Places.ToList(),
                Republic = _db.Republics.ToList(),
                City = _db.Cities.ToList()
            };
            ViewBag.HostPath = _appEnvironment.WebRootPath;
            return View(model);
        }
        [Authorize(Roles ="Admin")]
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