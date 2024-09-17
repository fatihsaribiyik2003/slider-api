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
    public enum enumprotokolmainsurec
    {
        [Display(Name = "Eğitim ve Öğretim")]
        EgitimveOgretim = 1,
        [Display(Name = "Ar-Ge")]
        ArGe = 2,
        [Display(Name = "Uygulama ve Topluma Hizmet")]
        UygulamaveToplumaHizmet = 3,
        [Display(Name = "İdari ve Destek")]
        İdarivedestek = 4,
        [Display(Name = "Tüm Süreçler(Genel İşbirliği)")]
        TumSurecler = 5,
        
    }

    public static class EnumExtensionsmainsurec
    {
        public static string GetDisplayNamemainsurec(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
