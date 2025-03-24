using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class BalanceController : Controller
    {
        private readonly IBalanceService _service;

        private readonly UserManager<Users> _userManager;

        public BalanceController(IBalanceService service, UserManager<Users> usermanager)
        {
            _service = service;

            _userManager = usermanager;
        }

        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            var user = await _userManager.GetUserAsync(User);

            var userId = user.Id;

            if(userId is null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = await _service.GetBalance(userId);

            if (!response.success)
            {
                Console.WriteLine("Error ==> " + response.Message);

                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine("Success ==> " + response.Message);

            var data = new DepositMoneyVM()
            {
                UserBalance = response.Data
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(DepositMoneyVM vm)
        {
            var user = await _userManager.GetUserAsync(User);

            var userId = user.Id;

            var response = await _service.DepositMoney(vm.Amount, userId);

            if (!response.success)
            {
                Console.WriteLine("Error ==> " + response.Message);
            }

            Console.WriteLine("Success ==> " + response.Message);

            return RedirectToAction("PersonalInfo", "PersonalInfo");
        }

        [HttpGet]
        public async Task<IActionResult> Withdraw()
        {
            var user = await _userManager.GetUserAsync(User);

            var userId = user.Id;

            if (userId is null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = await _service.GetBalance(userId);

            if (!response.success)
            {
                Console.WriteLine("Error ==> " + response.Message);

                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine("Success ==> " + response.Message);

            var data = new DepositMoneyVM()
            {
                UserBalance = response.Data
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(DepositMoneyVM vm)
        {
            var user = await _userManager.GetUserAsync(User);

            var userId = user.Id;

            var response = await _service.WithdrawMoney(vm.Amount, userId);

            if (!response.success)
            {
                Console.WriteLine("Error ==> " + response.Message);
            }

            Console.WriteLine("Success ==> " + response.Message);

            return RedirectToAction("PersonalInfo", "PersonalInfo");
        }
    }
}
