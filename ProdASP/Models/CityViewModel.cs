using System.ComponentModel.DataAnnotations;

namespace ProdASP.Models
{
    public class CityViewModel
    {
        [Required]
        public string NamePlace { get; set; } = null!;
        [Required]
        public int CountryName { get; set; }
        [Required]
        public string RepubName { get; set; }
    }
}
