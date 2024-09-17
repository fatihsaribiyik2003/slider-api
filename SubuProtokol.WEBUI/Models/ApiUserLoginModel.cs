using System.ComponentModel.DataAnnotations;

namespace SubuProtokol.WEBUI.Models
{
    public class ApiUserLoginModel
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
