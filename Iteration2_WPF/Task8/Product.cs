using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public enum ProductTypes
    {
        Food,
        Device
    }
    public class Product
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Icon { get; set; }
        public ProductTypes ProductType { get; set; }

    }
}
