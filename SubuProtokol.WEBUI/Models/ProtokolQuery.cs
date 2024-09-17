using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using SubuProtokol.Core.Enums;

namespace SubuProtokol.WEBUI.Models
{
    public class ProtokolQuery
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ProtokolStartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ProtokolFinishTime { get; set; }
        public string? ProtokoldenSorumluKisi { get; set; }
        public string? ProtokolImzalananKurum { get; set; }
        public EnumProtokolUnderType UnderType { get; set; }
        public enumprotokolmainsurec MainSurec { get; set; }
        public EnumDepartmentType DepartmantType { get; set; }
        public EnumProtokolMainType MainType { get; set; }
        public EnumProtokolScope Scope { get; set; }
        public EnumSector Sector { get; set; }
        public EnumProtokolTime Time { get; set; }
        public string? indirimAciklamasi { get; set; }
        //public int UnitId { get; set; }
        public string protokoldensorumlubirim { get; set; }
        public string? FileKey { get; set; }
        [Display(Name = "Dosya")]
        public IFormFile? File { get; set; }
        public string? Konu { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public string? EmailImzalananKurum { get; set; }
        public int? protokolfiltreleme { get; set; }
        public int? parentprotokolid { get; set; }
        public bool? silindimi { get; set; }
        public bool? arsiv { get; set; }


    }
    public class ProtokolModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ProtokolStartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ProtokolFinishTime { get; set; }
        public string? ProtokoldenSorumluKisi { get; set; }
        public string? ProtokolImzalananKurum { get; set; }
        public EnumProtokolUnderType UnderType { get; set; }
        public enumprotokolmainsurec MainSurec { get; set; }
        public EnumDepartmentType DepartmantType { get; set; }
        public EnumProtokolMainType MainType { get; set; }
        public EnumProtokolScope Scope { get; set; }
        public EnumSector Sector { get; set; }
        public EnumProtokolTime Time { get; set; }
        public string? indirimAciklamasi { get; set; }
        public string? FileKey { get; set; }
        //[Display(Name = "Dosya")]
        //public IFormFile? File { get; set; }
        public string? Konu { get; set; }
        //public int UnitId { get; set; }
        public string protokoldensorumlubirim { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public string? EmailImzalananKurum { get; set; }
        public int? protokolfiltreleme { get; set; }
        public int? parentprotokolid { get; set; }
        public bool? silindimi { get; set; }
        public bool? arsiv { get; set; }


    }
}
