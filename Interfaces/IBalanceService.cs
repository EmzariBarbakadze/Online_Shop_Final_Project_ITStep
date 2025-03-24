using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Interfaces
{
    public interface IBalanceService
    {
        public Task<ServiceResponse<UserBalances>> GetBalance(string userId);

        public Task<ServiceResponse<decimal>> DepositMoney(decimal amount, string userId);

        public Task<ServiceResponse<decimal>> WithdrawMoney(decimal amount, string userId);
    }
}
