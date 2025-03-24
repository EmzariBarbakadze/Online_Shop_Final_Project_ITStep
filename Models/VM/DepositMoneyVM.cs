using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class DepositMoneyVM
    {
        public UserBalances? UserBalance { get; set; }

        public decimal Amount { get; set; } = 0;

        public ISOs ISO { get; set; } = ISOs.GEL;
    }
}
