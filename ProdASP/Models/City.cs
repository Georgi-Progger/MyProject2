using Microsoft.Build.Framework;

namespace ProdASP.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string? NamePlace { get; set; }
        [Required]
        public int CountryName { get; set; }
        [Required]
        public string RepubName { get; set; }
    }
}
