using SSH.Domain.Request.Brand;
using SSH.Domain.Response.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.BAL.Interface
{
    public interface IBrandService
    {
        SaveBrandRes SaveProductRes(SaveBrandReq brand);
        IEnumerable<Brand> GetData();
        Brand GetBrandByID(int Id);
        bool RemoveBrand(int Id);
        IList<Brand> ListBrand();
    }
}
