using SubuProtokol.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SubuProtokol.Models.Filter
{
    public class ProtokolGetAllFilter
    {
        public string? FileKey;

        public int? Id { get; set; }

        public EnumSector sectorId { get; set; }

        public string ProtokolSigned { get; set; }


        public string ProtokolUnderType { get; set; }


        public int? UnitId { get; set; }
        public int? TicketTypeId { get; set; }
        //public EnumTicketStatus? TicketStatusId { get; set; }

        public EnumDepartmentType departmantTypeId { get; set; }

        public EnumProtokolScope scopeId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnumProtokolMainType? MainType { get; set; }

        public string? Email { get; set; }
    }
}
