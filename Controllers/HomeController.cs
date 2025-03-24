using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;
using System.Diagnostics;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _service;

        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public HomeController(ILogger<HomeController> logger, IHomeService service, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _logger = logger;

            _service = service;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products_favProducts = new HomeIndexVM();
            products_favProducts.Products = await _service.GetAllProducts();

            products_favProducts.FavouriteProducts = await _service.GetFavouriteProducts();

            products_favProducts.User = await _userManager.GetUserAsync(User);

            if(products_favProducts == null)
            {
                return View();
            }

            return View(products_favProducts);
        }

        [HttpGet]
        public async Task<IActionResult> AddToFavourites(int productId)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                Console.WriteLine("User not logged in");

                return RedirectToAction("Index", "Home");
            }

            var response = await _service.AddToFavourites(productId, user.Id);

            if (!response.success)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
