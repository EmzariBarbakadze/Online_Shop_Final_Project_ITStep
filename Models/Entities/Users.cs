using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online_Shop_Final_Project_ITStep.Models.Entities
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreateDate { get; set; } = DateTime.Now;


        // --------------------

        public List<Products> Product { get; set; }
        public Carts Cart { get; set; }
        public List<Orders> Order { get; set; }
        public UserBalances UserBalance { get; set; }
        public List<FavouriteProducts> FavouriteProduct { get; set; }
    }
}
