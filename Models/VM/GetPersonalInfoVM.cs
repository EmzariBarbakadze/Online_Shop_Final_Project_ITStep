using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class GetPersonalInfoVM
    {
        public Users User { get; set; }

        public UserBalances UserBalance { get; set; }

        public List<Orders>? Orders { get; set; }
    }
}
