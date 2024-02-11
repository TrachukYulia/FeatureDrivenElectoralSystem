using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Feature: BaseEntity
    {
        public int CharacteristicsId { get; set; }  
        public Characteristics Characteristics { get; set; }
    }
}
