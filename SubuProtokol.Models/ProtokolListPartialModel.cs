using Microsoft.AspNetCore.Http;
using SubuProtokol.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubuProtokol.Models
{
    public class ProtokolListPartialModel
    {
        

        //[DataType(DataType.Date)]
        //[DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public int Id { get; set; }
        public string? Email { get; set; }
        public int Telefon { get; set; }
        public DateTime? ProtokolStartTime { get; set; }
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
        public string protokoldensorumlubirim { get; set; }
    }
}
