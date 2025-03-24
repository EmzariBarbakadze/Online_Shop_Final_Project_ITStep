using System.ComponentModel.DataAnnotations;

namespace Online_Shop_Final_Project_ITStep.Models.VM
{
    public class UserSignUpVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match each other")]
        public string RepeatPassword { get; set; }

        public bool StaySignedIn { get; set; } = false;
    }
}
