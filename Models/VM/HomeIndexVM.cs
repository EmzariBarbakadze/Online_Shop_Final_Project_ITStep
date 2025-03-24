using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class HomeIndexVM
    {
        public List<Products> Products { get; set; }

        public List<FavouriteProducts> FavouriteProducts { get; set; }

        public Users? User { get; set; }
    }
}
