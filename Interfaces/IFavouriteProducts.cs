using Online_Shop_Final_Project_ITStep.Models;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface IFavouriteProducts
    {
        public Task<ServiceResponse<string>> GetFavourites(int productId);
    }
}
