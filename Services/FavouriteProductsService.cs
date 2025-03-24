using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class FavouriteProductsService : IFavouriteProducts
    {
        private readonly ApplicationDbContext _context;

        public FavouriteProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<string>> GetFavourites(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
