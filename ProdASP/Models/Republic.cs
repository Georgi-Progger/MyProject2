using System.ComponentModel.DataAnnotations;

namespace ProdASP.Models
{
    public class Republic
    {
        public int Id { get; set; }
        public string? NamePlace { get; set; }
        [Required]
        public string? Language { get; set; }
        public int CountryName { get; set; }
    }
}
