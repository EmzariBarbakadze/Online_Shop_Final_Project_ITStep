using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Products>> AddToFavourites(int productId, string userId)
        {            
            var response = new ServiceResponse<Products>();

            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                response.success = false;
                response.Message = "Can not find user";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                response.success = false;
                response.Message = "Can not find product";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            var favouriteProduct = await _context.FavouriteProducts.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

            if(favouriteProduct == null)
            {
                var newFavourteProduct = new FavouriteProducts()
                {
                    UserId = userId,
                    ProductId = productId
                };

                await _context.FavouriteProducts.AddAsync(newFavourteProduct);
                await _context.SaveChangesAsync();

                response.Data = product;
                response.success = true;
                response.Message = "New favourite product created";

                Console.WriteLine("Success ==> " + response.Message);

                return response;
            }

            if(favouriteProduct.IsActive == true)
            {
                favouriteProduct.IsActive = false;

                _context.FavouriteProducts.Update(favouriteProduct);
                await _context.SaveChangesAsync();

                response.Data = product;
                response.success = true;
                response.Message = "Favourite product disabled";

                Console.WriteLine("Success ==> " + response.Message);

                return response;
            }

            favouriteProduct.IsActive = true;

            _context.FavouriteProducts.Update(favouriteProduct);
            await _context.SaveChangesAsync();

            response.Data = product;
            response.success = true;
            response.Message = "Favourite product enabled";

            Console.WriteLine("Success ==> " + response.Message);

            return response;
        }

        public async Task<List<Products>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            if(products == null)
            {
                Console.WriteLine("No products found");

                return new List<Products>();
            }

            return products;
        }

        public async Task<List<FavouriteProducts>> GetFavouriteProducts()
        {
            var favouriteProducts = await _context.FavouriteProducts.ToListAsync();

            if(favouriteProducts == null)
            {
                Console.WriteLine("No favourite products found");

                return new List<FavouriteProducts>();
            }

            return favouriteProducts;
        }
    }
}
