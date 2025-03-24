using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;
using Online_Shop_Final_Project_ITStep.Models.VM;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly ApplicationDbContext _context;

        public PersonalInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<GetPersonalInfoVM>> GetPersonalInfo(string userId)
        {
            var response = new ServiceResponse<GetPersonalInfoVM>();
            var personalVM = new GetPersonalInfoVM();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var userBalance = await _context.Userbalances.FirstOrDefaultAsync(x => x.UserId == userId);

            if(user == null)
            {
                response.success = false;
                response.Message = "Authenticated user not found";

                return response;
            }

            if(userBalance == null)
            {
                response.success = false;
                response.Message = "No balance for this user found";

                return response;
            }

            personalVM.User = user;
            personalVM.UserBalance = userBalance;
            personalVM.Orders = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();

            response.Data = personalVM;
            response.success = true;
            response.Message = "Personal info taken successfully";

            return response;
        }
    }
}
