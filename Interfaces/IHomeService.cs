using Microsoft.Identity.Client;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface IHomeService
    {
        public Task<List<Products>> GetAllProducts();

        public Task<List<FavouriteProducts>> GetFavouriteProducts();

        public Task<ServiceResponse<Products>> AddToFavourites(int productId, string userId);
    }
}
