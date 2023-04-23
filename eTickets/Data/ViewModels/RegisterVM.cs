using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full name")]
        [Required]
        public string FullName { get; set; }
        [Display(Name = "Email address")]
        [Required]
        public string EmailAddress { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password's don't match")]
        public string ConfirmPassword { get; set; }
    }
}
