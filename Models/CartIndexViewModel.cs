﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmStore0906.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}