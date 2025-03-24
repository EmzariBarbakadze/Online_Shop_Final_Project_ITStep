using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;
using System.Security.Claims;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class FavouriteProductsController : Controller
    {
        private readonly IHomeService _service;
        private readonly IFavouriteProducts _secondService;

        private readonly UserManager<Users> _userManager;

        public FavouriteProductsController(IHomeService service, UserManager<Users> usermanager, IFavouriteProducts secondService)
        {
            _service = service;
            _secondService = secondService;

            _userManager = usermanager;
        }

        [HttpGet]
        public async Task<IActionResult> FavProducts()
        {
            var products_favProducts = new HomeIndexVM();
            products_favProducts.Products = await _service.GetAllProducts();

            products_favProducts.FavouriteProducts = await _service.GetFavouriteProducts();

            products_favProducts.User = await _userManager.GetUserAsync(User);

            if (products_favProducts == null)
            {
                return View();
            }

            return View(products_favProducts);

        }

        public async Task<IActionResult> ModifyFavourites(int productId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                Console.WriteLine("User not logged in");

                return RedirectToAction("Index", "Home");
            }

            var response = await _service.AddToFavourites(productId, user.Id);

            if (!response.success)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("FavProducts", "FavouriteProducts");
        }
    }
}
