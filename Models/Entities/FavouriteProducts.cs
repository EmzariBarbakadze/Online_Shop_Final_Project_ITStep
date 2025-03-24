using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class FavouriteProducts
    {
        [Key]
        public int FavouriteProductId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime Created { get; set; } = DateTime.Now;

        // ---------------------------------------

        public Users User { get; set; }
        public Products Product { get; set; }
    }
}
