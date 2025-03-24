using Azure;
using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Context;
using Online_Shop_Final_Project_ITStep.Interfaces;
using Online_Shop_Final_Project_ITStep.Models;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly ApplicationDbContext _context;

        public BalanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<UserBalances>> GetBalance(string userId)
        {
            var response = new ServiceResponse<UserBalances>();

            if(userId is null)
            {
                response.success = false;
                response.Message = "User not found";

                return response;
            }

            var userBalance = await _context.Userbalances.FirstOrDefaultAsync(x => x.UserId == userId);

            if(userBalance is null)
            {
                response.success = false;
                response.Message = "Balance not found";

                return response;
            }

            response.success = true;
            response.Data = userBalance;
            response.Message = "Balance found successfully";

            return response;
        }

        public async Task<ServiceResponse<decimal>> DepositMoney(decimal amount, string userId)
        {
            var response = new ServiceResponse<decimal>();

            if(userId is null)
            {
                response.success = false;
                response.Message = "User not found";

                return response;
            }

            if(amount <= 0)
            {
                response.success = false;
                response.Message = "Amount must be more than zero";

                return response;
            }

            var balance = await _context.Userbalances.FirstOrDefaultAsync(x => x.UserId == userId);

            if(balance is null)
            {
                response.success = false;
                response.Message = "Balance not found";

                return response;
            }

            balance.Balance += amount;

            _context.Userbalances.Update(balance);
            await _context.SaveChangesAsync();

            response.Data = balance.Balance;
            response.success = true;
            response.Message = "Deposit was successful";

            return response;
        }

        public async Task<ServiceResponse<decimal>> WithdrawMoney(decimal amount, string userId)
        {
            var response = new ServiceResponse<decimal>();

            if(amount <= 0)
            {
                response.success = false;
                response.Message = "Amount must be more than zero";

                return response;
            }

            if (userId is null)
            {
                response.success = false;
                response.Message = "User not found";

                return response;
            }

            var userBalance = await _context.Userbalances.FirstOrDefaultAsync(x => x.UserId == userId);

            if(userBalance is null)
            {
                response.success = false;
                response.Message = "User balance not found";

                return response;
            }

            if(amount > userBalance.Balance)
            {
                response.success = false;
                response.Message = "Not enough money on balance";

                return response;
            }

            userBalance.Balance -= amount;

            _context.Userbalances.Update(userBalance);
            await _context.SaveChangesAsync();

            response.success = true;
            response.Message = "Withdrawal was successful";
            response.Data = userBalance.Balance;

            return response;
        }
    }
}
