using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly UserManager<Users> _userManager;

        public OrderController(IOrderService service, UserManager<Users> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = await _service.MakeOrder(user.Id);

            if (!response.success)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("PersonalInfo", "PersonalInfo");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var response = await _service.GetOrderItems(orderId);

            return View(response.Data);
        }
    }
}
