using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSH.Web.Models.Product
{
    public class ProductView
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}
