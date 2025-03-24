using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;
using Microsoft.AspNetCore.Identity;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class AddProductsService : IAddProductsService
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AddProductsService(ApplicationDbContext context, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _context = context;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<ProductCategories>> GetCategories()
        {
            var categories = await _context.Productcategories.ToListAsync();

            return categories;
        }

        public async Task<ServiceResponse<string>> AddProduct(AddProductVM vm)
        {
            var response = new ServiceResponse<string>();

            var user = await _userManager.GetUserAsync(_signInManager.Context.User);

            if(user == null)
            {
                response.success = false;
                response.Message = "Can not reach authenticated user";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            var product = new Products()
            {
                Name = vm.Name,
                Description = vm.Description,
                ProductCategoryId = vm.CategoryId,
                Price = vm.Price,
                ISO = vm.ISO,
                Stock = vm.Stock, 
                ProviderId = user.Id
            };

            if(product == null)
            {
                response.success = false;
                response.Message = "Can not create product";

                Console.WriteLine("Error ==> " + response.Message);

                return response;
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            response.Data = product.Name;
            response.success = true;
            response.Message = "Product created successfully";

            Console.WriteLine("Success ==> " + response.Message);

            return response;
        }
    }
}
