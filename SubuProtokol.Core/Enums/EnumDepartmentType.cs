using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubuProtokol.Core.Enums
{
    public enum    EnumDepartmentType
    {
        [Display(Name = "Üniversite")]
        Üniversite = 1,
        [Display(Name = "Sağlık Kurumu")]
        SaglikKurumu = 2,
        [Display(Name = "Sanayi Kurumu")]
        SanayiKurumu = 3,
        [Display(Name = "Akademik ve Araştırma Kurumları")]
        AkademikveAraştırmaKurumları = 4,
        [Display(Name = "Özel Şirket")]
        OzelSirket = 5,
        [Display(Name = "Eğitim Kurumu")]
        EgitimKurumu = 6,
        [Display(Name = "STK")]
        STK = 7,
        [Display(Name = "Diğer")]
        Diger = 8,
        [Display(Name = "Resmi Kurum")]
        ResmiKurum = 9
    }
    public static class EnumExtensionsdepartmenttype
    {
        public static string GetDisplayNamedepartmenttype(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
