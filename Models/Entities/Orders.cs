using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalPrice { get; set; }

        public ISOs ISO { get; set; } = ISOs.GEL;

        public string Status { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        // ------------------------

        public Users User { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
}
