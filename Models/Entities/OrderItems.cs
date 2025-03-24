using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class OrderItems
    {
        [Key]
        public int OrderItemId { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalItemsPrice { get; set; }
        public ISOs ISO { get; set; } = ISOs.GEL;

        // -----------------------------------

        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
