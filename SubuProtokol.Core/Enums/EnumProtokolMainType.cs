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
    public enum EnumProtokolMainType
    {
        [Display(Name = "Eğitim ve Belgelendirme")]
        EgitimBelgelendirme = 1,
        [Display(Name = "Genel İşbirliği")]
        GenelIsbirligi = 2,
        [Display(Name = "Değişim Programı")]
        DegisimProgrami = 3,
        [Display(Name = "İndirim Protokolü")]
        IndirimProtokolu = 4,
        [Display(Name = "Üniversite-İş Dünyası İşbirliği")]
        UniversiteIsDunyasiIsbirligi = 5,
        [Display(Name = "Danışmanlık")]
        Danismanlik = 6

    }
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (member == null)
            {
                return null; // veya hata mesajı döndürebilirsiniz.
            }

            var displayAttribute = member.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute == null)
            {
                return null; // veya varsayılan bir değer döndürebilirsiniz.
            }

            return displayAttribute.GetName();
        }
    }

}
