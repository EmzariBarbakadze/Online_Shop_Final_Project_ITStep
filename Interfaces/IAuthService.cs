using Microsoft.AspNetCore.Mvc;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface IAuthService
    {
        public Task<ServiceResponse<string>> SignUp(UserSignUpVM vm);

        public Task<ServiceResponse<string>> SignIn(UserSignInVM vm);

        public Task<ServiceResponse<string>> SignOut();
    }
}
