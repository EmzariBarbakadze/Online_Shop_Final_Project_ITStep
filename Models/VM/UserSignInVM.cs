using System.ComponentModel.DataAnnotations;

namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class UserSignInVM
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool StaySignedIn { get; set; } = false;
    }
}
