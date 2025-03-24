using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.VM;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using System.Runtime.CompilerServices;

namespace Online_Shop_Final_Project_ITStep.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpVM vm)
        {
            if (ModelState.IsValid)
            {
                var response = new ServiceResponse<string>();
                    
                response = await _service.SignUp(vm);

                if (response.success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(vm);
                }
            }
            else
                return View(vm);
        }

        public async Task<IActionResult> SignOut()
        {
            var response = new ServiceResponse<string>();

            response = await _service.SignOut();

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInVM vm)
        {
            if (ModelState.IsValid)
            {
                var response = new ServiceResponse<string>();

                response = await _service.SignIn(vm);

                if (!response.success)
                {
                    return View(vm);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(vm);
            }
        }
    }
}
