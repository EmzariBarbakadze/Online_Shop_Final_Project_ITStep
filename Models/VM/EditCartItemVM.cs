using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class EditCartItemVM
    {
        public int CartItemId { get; set; }

        public Products Product { get; set; }

        public int Quantity { get; set; }
    }
}
