﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NonLinearDigitalDemo.ViewModels
{
    public class CartViewModel
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}