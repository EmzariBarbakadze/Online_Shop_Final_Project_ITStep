using System.ComponentModel.DataAnnotations;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class ProductCategories
    {
        [Key]
        public int ProductCategoryId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime Created { get; set; } = DateTime.Now;

        // -------------------------------

        public List<Products> Products { get; set; }
    }
}
