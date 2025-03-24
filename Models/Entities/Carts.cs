using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class Carts
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalPrice { get; set; } = 0;

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        // -----------------------

        public Users User { get; set; }

        public List<CartItems> CartItems { get; set; }
    }
}
