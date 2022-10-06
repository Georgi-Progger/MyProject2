using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProdASP.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Country> placies, int place, string plc)
        {
            placies.Insert(0, new Country {NamePlace = "Все", Id = 0 });
            Places = new SelectList(placies, "Id", "NamePlace", place);
            SelectedPlace = place;
            SelectedName = plc;
        }
        public SelectList Places { get; } 
        public int SelectedPlace { get; } 
        public string SelectedName { get; } 
    }
}
