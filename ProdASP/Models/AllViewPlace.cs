namespace ProdASP.Models
{
    public class AllViewPlace
    {
        public IEnumerable<Country> Country { get; set; }
        public IEnumerable<Republic> Republic { get; set; }
        public IEnumerable<City> City { get; set; }
    }
}
