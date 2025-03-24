using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class AddCategoryService : IAddCategoryService
    {
        private readonly ApplicationDbContext _context;

        public AddCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> AddCategory(AddCategoryVM vm)
        {
            var response = new ServiceResponse<string>();

            if(await _context.Productcategories.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower())){

                response.success = false;
                response.Message = "Category already exists";

                Console.WriteLine("Error ==> " + response.Message);                

                return response;
            }

            var productCategory = new ProductCategories()
            {
                Name = vm.Name
            };

            await _context.Productcategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();

            response.Data = vm.Name;
            response.success = true;
            response.Message = "Product category added successfully";

            Console.WriteLine("Success ==> " + response.Message);

            return response;
        }
    }
}
