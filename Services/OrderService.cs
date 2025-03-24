using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
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

            if (userBalance.Balance < cart.TotalPrice)
            {
                response.success = false;
                response.Message = "ERROR --> No enough money on balance";

                Console.WriteLine(response.Message);

                return response;
            }

            await CutFromBalance(cart.TotalPrice, userId);

            var cartItems = await _context.CartItems
                .Where(x => x.CartId == cart.CartId)
                .ToListAsync();

            var totalPrice = 0;

            var order = new Orders()
            {
                UserId = userId,
                TotalPrice = totalPrice,
                ISO = ISOs.GEL,
                Status = "Ordered"
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(); 

            foreach (var obj in cartItems)
            {
                totalPrice += obj.TotalItemPrice;

                var tempProduct = await _context.Products
                    .FirstOrDefaultAsync(x => x.ProductId == obj.ProductId);

                if (tempProduct is not null)
                {
                    await TransferToBalance(obj.TotalItemPrice, tempProduct.ProviderId);
                }

                var orderItem = new OrderItems()
                {
                    OrderId = order.OrderId,
                    ProductId = obj.ProductId,
                    Quantity = obj.Quantity,
                    TotalItemsPrice = obj.TotalItemPrice,
                    ISO = obj.ISO
                };

                await _context.OrderItems.AddAsync(orderItem);
                _context.CartItems.Remove(obj);
            }

            order.TotalPrice = totalPrice;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            cart.TotalPrice = 0;

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            response.success = true;
            response.Message = "SUCCESS --> successful order";

            Console.WriteLine(response.Message);

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
