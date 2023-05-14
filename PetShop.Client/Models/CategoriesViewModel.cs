using PetShop.data.Models;

namespace PetShop.Client.Models
{
    public class CategoriesViewModel
    {
        public IEnumerable<Animal> Animals { get; set; }
        public int Category { get; set; }
    }
}
