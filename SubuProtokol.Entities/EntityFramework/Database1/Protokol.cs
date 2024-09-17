using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubuProtokol.Core.Enums;
using SubuProtokol.Entities.Base;
using SubuProtokol.Models;
using System.ComponentModel;

namespace SubuProtokol.Entities.EntityFramework.Database1
{
    public class Protokol : EntityBase<int>
    {
        public string? Email { get; set; }
        public string? EmailImzalananKurum { get; set; }
        public string? Telefon { get; set; }
        public DateTime? ProtokolStartTime { get; set; }
        public DateTime? ProtokolFinishTime { get; set; }
        public string? ProtokoldenSorumluKisi { get; set; }
        public string? ProtokolImzalananKurum { get; set; }
        public EnumProtokolUnderType? UnderType { get; set; }
        public enumprotokolmainsurec? MainSurec { get; set; }
        public EnumDepartmentType? DepartmantType { get; set; }
        public EnumProtokolMainType? MainType { get; set; }
        public EnumProtokolScope? Scope { get; set; }
        public EnumSector? Sector { get; set; }
        public EnumProtokolTime? Time { get; set; }
        public string? indirimAciklamasi { get; set; }
        //public string? protokoldensorumlubirim { get; set; }
        public string? FileKey { get; set; }
        public string? Konu { get; set; }
        public virtual Unit Unit { get; set; }
        public int UnitId { get; set; }
        [DefaultValue(false)]
        public bool MailKontrol { get; set; }
        public int? protokolfiltreleme { get; set; }
        public int? parentprotokolid { get; set; }
        public bool? silindimi { get; set; }
        public bool? arsiv { get; set; }

    }
}
