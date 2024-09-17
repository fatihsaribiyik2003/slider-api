using SubuProtokol.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SubuProtokol.WEBUI.Models
{
    public class ApiUserModel
    {
        public int? Id { get; set; }
        [Required]
        public string UserName { get; set; }
        //public string AssociatedUserKey { get; set; } 

        [Required]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        [Display(Name = "Soyad")]
        [Required]
        public string FamilyName { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen eposta giriniz.")]
        [Required(ErrorMessage = "Eposta Alanı Zorunludur.")]
        [Display(Name = "Eposta")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Required]
        public EnumUserRole UserRole { get; set; } = EnumUserRole.Admin;
        //public int UnitId { get; set; }
    }
}
