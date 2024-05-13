﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ItemRequest
    {
        public string Name { get; set; }
        public ICollection<ItemFeatureRequest> FeatureItem { get; set; }
    }
}
