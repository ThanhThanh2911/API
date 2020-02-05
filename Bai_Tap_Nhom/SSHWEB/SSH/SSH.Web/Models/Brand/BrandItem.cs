using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSH.Web.Models.Brand
{
    public class BrandItem
    {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public int ParenID { get; set; }
    }
}
