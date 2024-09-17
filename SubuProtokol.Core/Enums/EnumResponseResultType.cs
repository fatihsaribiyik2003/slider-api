using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Core.Enums
{
    public enum EnumResponseResultType
    {
        [EnumMember(Value = "Success")]
        Success = 1,
        [EnumMember(Value = "Error")]
        Error = 2,
        [EnumMember(Value = "Info")]
        Info = 3,
        [EnumMember(Value = "Warning")]
        Warning = 4
    }
}
