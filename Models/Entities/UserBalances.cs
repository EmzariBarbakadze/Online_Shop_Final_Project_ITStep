using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class UserBalances
    {
        [Key]
        public int UserBalanceId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Balance { get; set; }

        public ISOs ISO { get; set; } = ISOs.GEL;

        public bool IsActive { get; set; } = true;

        // ---------------------------

        public Users User { get; set; }
    }
}
