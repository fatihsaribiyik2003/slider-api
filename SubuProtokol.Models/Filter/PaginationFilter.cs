using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Models.Filter
{
    public class PaginationFilter
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
