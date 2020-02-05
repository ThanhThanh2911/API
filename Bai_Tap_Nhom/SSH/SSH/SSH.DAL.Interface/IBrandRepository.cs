using SSH.Domain.Request.Brand;
using SSH.Domain.Response.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.DAL.Interface
{
    public interface IBrandRepository
    {
        SaveBrandRes Created(SaveBrandReq brand);
        IEnumerable<Brand> GetData();
        Brand GetBrandByID(int Id);
        bool RemoveBrand(int Id);
        IList<Brand> ListBrand();
    }
}
