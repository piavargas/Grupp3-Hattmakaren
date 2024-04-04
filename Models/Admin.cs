using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class Admin
    {
        public int adminId { get; set; }    
        public string firstName { get; set; }
        public string lastName { get; set; }
        
        [Required(ErrorMessage = "Please write a username.")]
        [StringLength(50)]

        public string userName { get; set; }

        [Required(ErrorMessage = "Please write a password.")]
        [DataType(DataType.Password)]

        public string passWord { get; set; }
    }
}
