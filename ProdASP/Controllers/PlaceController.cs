﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            if (adv != null)
            {
                _db.Places.Remove(adv);
                _db.SaveChanges();
            }
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
    }
}
