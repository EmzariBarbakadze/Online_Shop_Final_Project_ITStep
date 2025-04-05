using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using System.Diagnostics;
using System.Net.WebSockets;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<OrderItems>>> GetOrderItems(int orderId)
        {
            var response = new ServiceResponse<List<OrderItems>>();

            var orderItems = await _context.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();

            response.success = true;
            response.Message = "SUCCESS --> order items successfully found";
            response.Data = orderItems;

            Console.WriteLine(response.Message);

            return response;
        }

        public async Task<ServiceResponse<List<CartItems>>> MakeOrder(string userId)
        {
            var response = new ServiceResponse<List<CartItems>>();

            if (userId is null)
            {
                response.success = false;
                response.Message = "ERROR --> arguments can not be zero or null";

                Console.WriteLine(response.Message);
                return response;
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            var userBalance = await _context.Userbalances.FirstOrDefaultAsync(x => x.UserId == userId);           

            if (cart is null || userBalance is null)
            {
                response.success = false;
                response.Message = "ERROR --> Cart or Balance not found";

                Console.WriteLine(response.Message);

                return response;
            }

            var cartItems = await _context.CartItems.Where(x => x.CartId == cart.CartId).ToListAsync();
            var orderPrice = await _context.CartItems.Where(x => x.CartId == cart.CartId).SumAsync(x => x.TotalItemPrice);

            if (cartItems.IsNullOrEmpty())
            {
                response.success = false;
                response.Message = "ERROR --> No cart item found";

                Console.WriteLine(response.Message);

                return response;
            }           
            
            if (userBalance.Balance < orderPrice)
            {
                response.success = false;
                response.Message = "ERROR --> No enough money on balance";

                Console.WriteLine(response.Message);

                return response;
            }

            await CutFromBalance(orderPrice, userId);

            var order = new Orders()
            {
                UserId = userId,
                TotalPrice = orderPrice,
                ISO = ISOs.GEL,
                Status = "Ordered"
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var obj in cartItems)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == obj.ProductId);
                await TransferToBalance(obj.TotalItemPrice, product.ProviderId);

                var orderItem = new OrderItems()
                {
                    OrderId = order.OrderId,
                    ProductId = obj.ProductId,
                    Quantity = obj.Quantity,
                    TotalItemsPrice = obj.TotalItemPrice
                };

                await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();

                product.Stock -= obj.Quantity;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                _context.CartItems.Remove(obj);
                await _context.SaveChangesAsync();
            }          

            return response;
        }

        private async Task TransferToBalance(decimal amount, string userId)
        {
            var balance = await _context.Userbalances.FirstOrDefaultAsync(x => x.UserId == userId);

            if (balance is not null)
            {
                balance.Balance += amount;
                _context.Userbalances.Update(balance);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CutFromBalance(decimal amount, string userId)
        {
            var balance = await _context.Userbalances.FirstOrDefaultAsync(x => x.UserId == userId);

            if (balance is not null)
            {
                balance.Balance -= amount;
                _context.Userbalances.Update(balance);
                await _context.SaveChangesAsync();
            }
        }        
    }
}
