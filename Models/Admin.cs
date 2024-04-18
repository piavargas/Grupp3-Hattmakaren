using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Grupp3Hattmakaren.Models
{
    public class Admin : User
    {
        public string employerCode { get; set; }

    }
}
