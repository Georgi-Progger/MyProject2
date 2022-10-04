using System.ComponentModel.DataAnnotations;

namespace ProdASP.Models
{
    public class PlaceViewModel
    {
        [Required]
        public string NamePlace { get; set; } = null!;
        [Required]
        public string Language { get; set; } = null!;
        [Required]
        public string? Information { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}
