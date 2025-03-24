using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface IAddCategoryService
    {
        public Task<ServiceResponse<string>> AddCategory(AddCategoryVM vm);
    }
}
