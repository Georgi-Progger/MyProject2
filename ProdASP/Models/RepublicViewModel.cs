using System.ComponentModel.DataAnnotations;

namespace ProdASP.Models
{
	public class RepublicViewModel 
	{
        [Required]
        public string NamePlace { get; set; } = null!;
        [Required]
        public string Language { get; set; } = null!;
        [Required]
        public string CountryName { get; set; }
	}
}
