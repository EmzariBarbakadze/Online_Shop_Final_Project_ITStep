using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models.VM;
using Online_Shop_Final_Project_ITStep.Services;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class AddProductsController : Controller
    {
        private readonly IAddProductsService _service;

        public AddProductsController(IAddProductsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var categoryList = await _service.GetCategories();
            
            var vm = new AddProductVM()
            {
                categories = categoryList
            };
            
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var response = await _service.AddProduct(vm);

            if (!response.success)
            {
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
