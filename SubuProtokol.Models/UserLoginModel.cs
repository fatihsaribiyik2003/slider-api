using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Models
{
    public class UserLoginModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        public string ReturnUrl { get; set; } = "";
        public bool RememberMe { get; set; } = false;

        public string? employeeType { get; set; }

        public string UserName { get; set; }

        public string UserRole { get; set; }

        public string Password { get; set; }
        public string? Email { get; set; }


       
    }
}
