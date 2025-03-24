using Online_Shop_Final_Project_ITStep.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class AddProductVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [NotNull, Required]
        public int CategoryId { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public ISOs ISO { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public List<ProductCategories> categories { get; set; } = new List<ProductCategories>();
    }
}
