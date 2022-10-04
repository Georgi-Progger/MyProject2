using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdASP.Data;
using ProdASP.Models;

namespace ProdASP.Controllers
{
    public class PlaceController : Controller
    {
        private ApplicationContext _db;
        private IWebHostEnvironment _appEnvironment;
        public PlaceController(ApplicationContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlaceViewModel plcViewModel)
        {
            if (plcViewModel != null)
            {
                string path;
                if (plcViewModel.Image != null)
                    path = $@"/images/{plcViewModel.Image.FileName}";
                else
                    path = "";
                string fullPath = _appEnvironment.WebRootPath + path;

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                if (plcViewModel.Image != null)
                {
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await plcViewModel.Image.CopyToAsync(fileStream);
                    }
                }

                Place adv = new Place
                {
                    NamePlace = plcViewModel.NamePlace,
                    Language = plcViewModel.Language,
                    Information = plcViewModel.Information,
                    Image = path,
                };
                _db.Places.Add(adv);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
