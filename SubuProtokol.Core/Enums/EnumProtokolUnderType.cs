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
    public  enum EnumProtokolUnderType
    {
        [Display(Name = "Eğitim, Sınav ve Belgelendirme")]
        EgitimSinavBelgelendirme = 1,
        [Display(Name = "Mesleki Eğitim Kursları")]
        MeslekiEgitimKursları = 2,
        [Display(Name = "Genel Çerçeve")]
        GenelCerceve = 3,
        [Display(Name = "Erasmus Değişim Programı")]
        ErasmusDEgisimProgrami = 4,
        [Display(Name = "Farabi Değişim Programı")]
        FarabiDegisimProgrami = 5,
        [Display(Name = "Mevlana Değişim Programı")]
        MevlanaDegisimProgrami = 6,
        [Display(Name = "Ürün/Hizmet İndirimi")]
        UrunHizmetIndirim = 7,
        [Display(Name = "İşletmede Mesleki Eğitim(+1)")]
        IsletmedeMeslekiEgitim = 8,
        [Display(Name = "Kurumsal Kaynak Paylaşımı")]
        KurumsalKaynakPaylasimi = 9,
        [Display(Name = "Sosyal & Kültürel Dayanışma")]
        SosyalKulturelDayanisma = 10,
        [Display(Name = "Mutabakat Zaptı")]
        MutabakatZabti =11,
        [Display(Name = "Proje Danışmanlığı")]
        Projedanismanligi = 12,
        [Display(Name = "Üretim ArGe Danışmanlığı")]
        UretimArGeDanismanligi = 13,
        [Display(Name = "Yönetim Danışmanlığı")]
        Yonetimdanismanligi = 14,
        [Display(Name = "Diğer Danışmanlıklar")]
        Digerdanismanliklar = 15,
        [Display(Name = "Diğer")]
        Diger = 16,
    }

    public static class EnumExtensionsUnderType
    {
        public static string GetDisplayNameUnderType(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
