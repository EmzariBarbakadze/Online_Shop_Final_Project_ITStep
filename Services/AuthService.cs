using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.VM;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using System.Security.Claims;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Users> _userManager;

        private readonly SignInManager<Users> _signInManager;


        public AuthService(ApplicationDbContext context, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _context = context;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ServiceResponse<string>> SignUp(UserSignUpVM vm)
        {
            var response = new ServiceResponse<string>();

            if(_context.User.Any(x => x.Email == vm.Email))
            {
                response.success = false;
                response.Message = "User already exists";

                Console.WriteLine("Error => " + response.Message);
                return response;
            }

            var user = new Users()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.Email,
                Email = vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                response.success = false;
                response.Message = "Unknown error";

                Console.WriteLine("Error => " + response.Message);

                return response;
            }

            var userBalance = new UserBalances()
            {
                UserId = user.Id,
                Balance = 0
            };

            var cart = new Carts()
            {
                UserId = user.Id
            };

            await _context.Userbalances.AddAsync(userBalance);
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();

            bool staySignedIn = vm.StaySignedIn ? true : false;

            var claims = new List<Claim>()
            {
                new Claim("FirstName", user.FirstName)
            };

            await _userManager.AddClaimsAsync(user, claims);

            await _signInManager.SignInAsync(user, isPersistent: staySignedIn);

            response.Data = user.FirstName;
            response.success = true;
            response.Message = "User registered sucessfully";

            Console.WriteLine("Success message ==> " + response.Message);            

            return response;
        }

        public async Task<ServiceResponse<string>> SignIn(UserSignInVM vm)
        {
            var response = new ServiceResponse<string>();

            if(!(_context.User.Any(x => x.Email == vm.Email))){
                response.success = false;
                response.Message = "No such user found";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            bool staySignedIn = vm.StaySignedIn ? true : false;

            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: staySignedIn, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                response.success = false;
                response.Message = "Unknown error";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            response.success = true;
            response.Message = "Successful login";

            Console.WriteLine("Success ==> " + response.Message);

            return response;
        }

        public async Task<ServiceResponse<string>> SignOut()
        {
            await _signInManager.SignOutAsync();

            var response = new ServiceResponse<string>();

            response.Data = string.Empty;
            response.success = true;
            response.Message = "Sign out successfully";

            return response;
        }
    }
}
