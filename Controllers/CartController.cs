using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _service;
        private readonly UserManager<Users> _userManager;

        public CartController(ICartService service, UserManager<Users> userManager)
        {
            _service = service;

            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddToCartPage(int id)
        {
            var response = await _service.GetProductById(id);

            if (!response.success) {
                return RedirectToAction("Index", "Home");
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(uint quantity, int productId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.GetUserAsync(User);

            var response = await _service.AddToCart(quantity, productId, user.Id);

            if (!response.success)
            {
                Console.WriteLine("Error ==> " + response.Message);

                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine("Success ==> " + response.Message);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> CartPage()
        {
            var user = await _userManager.GetUserAsync(User);

            var response = await _service.GetCartItems(user.Id);

            if (!response.success)
            {
                RedirectToAction("Index", "Home");
            }

            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            var response = await _service.RemoveFromCart(cartItemId);

            if (!response.success)
            {
                Console.WriteLine(response.Message);
            }

            Console.WriteLine(response.Message);

            return RedirectToAction("CartPage", "Cart");
        }

        [HttpGet]
        public async Task<IActionResult> EditCartItem(int cartItemId)
        {
            var response = await _service.GetCartItem(cartItemId);

            if (!response.success)
            {
                RedirectToAction("CartPage", "Cart");
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyChanges(int cartItemId, int quantity)
        {
            var response = await _service.UpdateCartItem(cartItemId, quantity);

            if (!response.success)
            {
                return View("EditCartItem");
            }

            return RedirectToAction("CartPage", "Cart");
        }
    }
}
