using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public string lastName { get; set; }

        [Display(Name = "User Role")]
        public string userRole { get; set; }

        [Display(Name = "Employer Code")]
        public string employerCode { get; set; }

        [Display(Name = "Head size")]
        public string headSize { get; set; }
    }
}
