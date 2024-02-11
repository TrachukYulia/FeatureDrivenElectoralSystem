using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Item: BaseEntity
    {
        public ICollection<Characteristic>?  Characteristics {get; set; }
    }
}
