using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Domain.Request.Brand
{
    public class SaveBrandReq
    {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public int ParenID { get; set; }
    }
}
