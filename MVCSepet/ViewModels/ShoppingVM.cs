using MVCSepet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepet.ViewModels
{
    public class ShoppingVM
    {
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
    }
}