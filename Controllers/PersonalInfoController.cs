using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Services;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly IPersonalInfoService _service;

        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public PersonalInfoController(IPersonalInfoService service, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _service = service;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> PersonalInfo()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var user =  await _userManager.GetUserAsync(User);

            if(user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = await _service.GetPersonalInfo(user.Id);

            if (!response.success)
            {
                Console.WriteLine("Error ==> " + response.Message);

                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine("Success ==> " + response.Message);

            return View(response.Data);
        }
    }
}
