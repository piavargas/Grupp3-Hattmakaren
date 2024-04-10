using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv ett användarnamn.")]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vänligen skriv lösenord.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Vänligen bekräfta lösenordet.")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenordet")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vänligen fyll i ditt förnamn.")]

        public string firstName { get; set; }

        [Required(ErrorMessage = "Vänligen fyll i ditt efternamn.")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Var vänlig och välj roll")]
        [Display(Name = "Användarroll")]
        public string userRole { get; set; }

        public int employerCode { get; set; }

    }
}
