using Microsoft.Build.Framework;

namespace ProdASP.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        [Required]
        public string? NamePlace { get; set; }
        [Required]
        public string? Information { get; set; }
        [Required]
        public string? Language { get; set; }
    }
}
