using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmStore0906.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Fruit> AllFruits { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}