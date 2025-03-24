namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class CartItemVM
    {
        public int CartItemId { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public ISOs ISO { get; set; } = ISOs.GEL;
    }
}
