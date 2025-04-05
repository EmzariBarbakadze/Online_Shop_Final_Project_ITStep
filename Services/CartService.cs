using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;
using System.Diagnostics;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<decimal>> AddToCart(uint quantity, int productId, string userId)
        {
            var response = new ServiceResponse<decimal>();

            if (quantity is 0 || productId is 0 || userId is null)
            {
                response.success = false;
                response.Message = "Parameters can not be null";

                return response;
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if(product is null)
            {
                response.success = false;
                response.Message = "No product with this id found";

                return response;
            }

            if(product.Stock < quantity)
            {
                response.success = false;
                response.Message = "Not enough products in the stock";

                return response;
            }

            var userCart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);

            if(userCart == null)
            {
                response.success = false;
                response.Message = "No cart found";

                return response;
            }

            var cartItems = await _context.CartItems.Where(x => x.CartId == userCart.CartId).ToListAsync();

            bool success = false;

            foreach (var item in cartItems)
            {
                if (item.ProductId == productId)
                {
                    item.Quantity += ((int)quantity);
                    item.TotalItemPrice += product.Price * ((int)quantity);

                    _context.Carts.Update(userCart);
                    _context.CartItems.Update(item);
                    await _context.SaveChangesAsync();

                    success = true;
                    break;
                }
            }

            if (!success)
            {
                var cartItem = new CartItems()
                {
                    CartId = userCart.CartId,
                    ProductId = productId,
                    Quantity = ((int)quantity),
                    TotalItemPrice = product.Price * ((int)quantity)
                };

                _context.Carts.Update(userCart);
                await _context.CartItems.AddAsync(cartItem);
                await _context.SaveChangesAsync();
            }

            response.success = true;
            response.Message = "Operation was successful";

            return response;

        }

        public async Task<ServiceResponse<Products>> GetProductById(int productId)
        {
            var response = new ServiceResponse<Products>();

            if(productId is 0)
            {
                response.success = false;
                response.Message = "Product id can not be zero";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if(product is null)
            {
                response.success = false;
                response.Message = "Product not found";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            response.success = true;
            response.Message = "Product successfully found";
            response.Data = product;

            Console.WriteLine("Success ==> " + response.Message);

            return response;
        }

        public async Task<ServiceResponse<decimal>> RemoveFromCart(int cartItemId)
        {
            var response = new ServiceResponse<decimal>();
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.CartItemId == cartItemId);

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            response.success = true;
            response.Message = "SUCCESS --> cart item successfully deleted";

            return response;
        }

        public async Task<ServiceResponse<List<CartItemVM>>> GetCartItems(string userId)
        {
            var response = new ServiceResponse<List<CartItemVM>>();

            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);

            var cartId = cart.CartId;

            if (cartId is 0)
            {
                response.success = false;
                response.Message = "ERROR --> cart id can not be zero";

                Console.WriteLine(response.Message);

                return response;
            }

            var cartItems = await _context.CartItems.Where(x => x.CartId == cartId).ToListAsync();

            var resultData = new List<CartItemVM>();

            foreach(var obj in cartItems)
            {
                resultData.Add(
                        new CartItemVM()
                        { 
                            CartItemId = obj.CartItemId,
                            Name = _context.Products.FirstOrDefault(x => x.ProductId == obj.ProductId).Name,
                            Quantity = obj.Quantity,
                            Price = obj.TotalItemPrice,
                            ISO = obj.ISO
                        }
                 );
            }

            response.success = true;
            response.Message = "SUCCESS --> successful operation";
            response.Data = resultData;

            Console.WriteLine(response.Message);

            return response;
        }

        public async Task<ServiceResponse<EditCartItemVM>> GetCartItem(int cartItemId)
        {
            var response = new ServiceResponse<EditCartItemVM>();

            if (cartItemId is 0)
            {
                response.success = false;
                response.Message = "Cart item id can not be zero";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }
            
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.CartItemId == cartItemId);

            if (cartItem is null)
            {
                response.success = false;
                response.Message = "Cart item not found";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == cartItem.ProductId);

            if (product is null)
            {
                response.success = false;
                response.Message = "Product not found";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            response.success = true;
            response.Message = "Cart item successfully found";
            response.Data = new EditCartItemVM()
            {
                CartItemId = cartItemId,
                Product = product,
                Quantity = cartItem.Quantity
            };

            Console.WriteLine("Success ==> " + response.Message);

            return response;
        }        

        public async Task<ServiceResponse<string>> UpdateCartItem(int cartItemId, int quantity)
        {
            var response = new ServiceResponse<string>();

            if(cartItemId is 0 || quantity is 0)
            {
                response.success = false;
                response.Message = "ERROR --> parameters can not be zero";

                Console.WriteLine(response.Message);

                return response;
            }

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.CartItemId == cartItemId);

            if(cartItem is null)
            {
                response.success = false;
                response.Message = "ERROR --> no cart item found";

                Console.WriteLine(response.Message);

                return response;
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == cartItem.ProductId);

            cartItem.Quantity = quantity;
            cartItem.TotalItemPrice = product.Price * quantity;

            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            var cartItems = await _context.CartItems.Where(x => x.CartId == cartItem.CartId).ToListAsync();

            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CartId == cartItem.CartId);

            var cartTotalPrice = 0;

            foreach(var obj in cartItems)
            {
                cartTotalPrice += obj.TotalItemPrice;
            }


            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            response.success = true;
            response.Message = "SUCCESS --> item updated successfully";

            return response;
        }
    }
}
