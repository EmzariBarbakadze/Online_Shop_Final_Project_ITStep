using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface IOrderService
    {
        public Task<ServiceResponse<List<CartItems>>> MakeOrder(string userId);

        public Task<ServiceResponse<List<OrderItems>>> GetOrderItems(int orderId);
    }
}
