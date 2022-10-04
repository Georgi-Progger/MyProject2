using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProdASP.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Place> companies, int company, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new Place {NamePlace = "Все", Id = 0 });
            Places = new SelectList(companies, "Id", "NamePlace", company);
            SelectedPlace = company;
            SelectedName = name;
        }
        public SelectList Places { get; } 
        public int SelectedPlace { get; } 
        public string SelectedName { get; } // введенное имя
    }
}
