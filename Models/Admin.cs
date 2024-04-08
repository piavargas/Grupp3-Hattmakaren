using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Grupp3Hattmakaren.Models
{
    public class Admin : IdentityUser
    {
        [Key]   
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        
    }
}
