using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdASP.Data;
using ProdASP.Models;

namespace ProdASP.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationContext _db;
        public AdminController(ApplicationContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(string name, int company = 0, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<Country> places = _db.Places.AsQueryable();

            if (company != 0)
            {
                places = places.Where(p => p.Id == company);
            }
            if (!string.IsNullOrEmpty(name))
            {
                places = places.Where(p => p.NamePlace!.Contains(name));
            }

            // сортировка
            places = sortOrder switch
            {
                SortState.NameAsc => places.OrderBy(s => s.NamePlace),
                SortState.NameDesc => places.OrderByDescending(s => s.NamePlace)
            };

            // пагинация
            var count = await places.CountAsync();
            var items = await places.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(_db.Places.ToList(), company, name),
                new SortViewModel(sortOrder)
            );
            return View(viewModel);
        }
    }
}
