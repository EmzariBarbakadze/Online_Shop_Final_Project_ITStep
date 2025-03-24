using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public int ProductCategoryId { get; set; }

        [ForeignKey("Id")]
        public string ProviderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public ISOs ISO { get; set; }

        [Range(0, 6)]
        public int Rating { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime Created { get; set; } = DateTime.Now;

        // -------------------------------

        public Users User { get; set; }
        public List<CartItems> CartItem { get; set; }
        public List<FavouriteProducts> FavouriteProducts { get; set; }
        public ProductCategories ProductCategory { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
}
