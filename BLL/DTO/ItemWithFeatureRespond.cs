using AutoMapper.Features;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ItemWithFeatureRespond
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FeatureItem> FeatureItem { get; set; }

    }
}
