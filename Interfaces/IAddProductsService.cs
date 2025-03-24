using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface IAddProductsService
    {
        public Task<List<ProductCategories>> GetCategories();

        public Task<ServiceResponse<string>> AddProduct(AddProductVM vm);
    }
}
