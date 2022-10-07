using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProdASP.Data;
using ProdASP.Models;
using System.IO;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Country? ctr = await _db.Places.FirstOrDefaultAsync(p => p.Id == id);
                if (ctr != null)
                {
                    return View(ctr);
                }
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Country plc, PlaceViewModel plcViewModel)
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
                else
                {
                    path = "";
                }
                _db.Places.Update(plc);
                plc.Image = path;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index","Admin");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delate(int id)
        {
            Country adv = _db.Places.FirstOrDefault(a => a.Id == id)!;
            var cit = from com in _db.Cities
                      where com.CountryName == adv.Id
                      select com;
            var rep = from com in _db.Republics
                      where com.CountryName == adv.Id
                      select com;
            if (adv != null)
            {
                _db.Places.Remove(adv);

                _db.SaveChanges();
            }
            foreach (City comment in cit)
            {
                _db.Cities.Remove(comment);
            }
            foreach (Republic comment in rep)
            {
                _db.Republics.Remove(comment);
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");

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

                Country adv = new Country
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
        public IActionResult CreateRepublic()
        {
            ViewData["Id"] = new SelectList(_db.Places, "Id", "NamePlace");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRepublic(RepublicViewModel plcViewModel)
        {
            if (plcViewModel != null)
            {
                Republic adv = new Republic
                {
                    NamePlace = plcViewModel.NamePlace,
                    Language = plcViewModel.Language,
                    CountryName = plcViewModel.CountryName
                    
                };
                _db.Republics.Add(adv);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult CreateCity()
        {
            ViewBag.Id = new SelectList(_db.Places,"Id", "NamePlace");
            ViewBag.Republic = new SelectList(_db.Republics, "Id", "NamePlace");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCity(CityViewModel plcViewModel)
        {
            if (plcViewModel != null)
            {
                City adv = new City
                {
                    NamePlace = plcViewModel.NamePlace,
                    CountryName = plcViewModel.CountryName,
                    RepubName = plcViewModel.RepubName

                };
                _db.Cities.Add(adv);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
