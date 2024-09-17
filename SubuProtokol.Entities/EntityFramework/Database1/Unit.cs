using SubuProtokol.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Entities.EntityFramework.Database1
{
    public class Unit : EntityBase<int>
    {

     
       
        public string BirimAd { get; set; }
        public string BirimTuruAd { get; set; }
        public int SabisBirimId { get; set; }
        public virtual ICollection<Protokol> Protokols { get; set; }
    }
}
