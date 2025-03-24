using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class CartItems
    {
        [Key]
        public int CartItemId { get; set; }

        [ForeignKey("CartId")]
        public int CartId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalItemPrice { get; set; }
        public ISOs ISO { get; set; } = ISOs.GEL;


        // ------------------------------

        public Products Product { get; set; }
        public Carts Cart { get; set; }
    }
}
