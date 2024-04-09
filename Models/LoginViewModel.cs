using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv ett användarnamn.")]
        [StringLength(50)] //Validering

        public string UserName { get; set; }

        [Required(ErrorMessage = "Vänligen skriv lösenord.")]
        [DataType(DataType.Password)]

        public string PassWord { get; set; }
    }
}
