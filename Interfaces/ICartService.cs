using Online_Shop_Final_Project_ITStep.Models.VM;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface ICartService
    {
        public Task<ServiceResponse<Products>> GetProductById(int productId);

        public Task<ServiceResponse<decimal>> AddToCart(int quantity, int productId, string userId);

        public Task<ServiceResponse<decimal>> RemoveFromCart(int cartItemId);

        public Task<ServiceResponse<List<CartItemVM>>> GetCartItems(string userId);

        public Task<ServiceResponse<EditCartItemVM>> GetCartItem(int cartItemId);

        public Task<ServiceResponse<string>> UpdateCartItem(int cartItemId, int quantity);
    }
}
