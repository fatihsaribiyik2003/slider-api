using SubuProtokol.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Models
{

    public interface IResult
    {
        public bool Success { get; }
        public string Message { get; }
       
        public EnumResponseResultType Type { get; set; }
    }
    public class Result :IResult
    {

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public EnumResponseResultType Type { get; set; }
        public Result(bool success)
        {
            Success = success;
        }
       
        public bool Success { get; }

        public string Message { get; }
    }
}