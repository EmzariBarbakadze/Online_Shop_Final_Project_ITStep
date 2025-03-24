using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class AddCategoryController : Controller
    {
        private readonly IAddCategoryService _service;

        public AddCategoryController(IAddCategoryService service)
        {
            _service = service;
        }
        
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var response = await _service.AddCategory(vm);

            if (!response.success)
            {
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
