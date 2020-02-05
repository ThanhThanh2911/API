using SSH.BAL.Interface;
using SSH.DAL.Interface;
using SSH.Domain.Request.Brand;
using SSH.Domain.Response.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.BAL
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public Brand GetBrandByID(int Id)
        {
            return _brandRepository.GetBrandByID(Id);
        }

        public IEnumerable<Brand> GetData()
        {
            return _brandRepository.GetData();
        }

        public IList<Brand> ListBrand()
        {
            return _brandRepository.ListBrand();
        }

        public bool RemoveBrand(int Id)
        {
            return _brandRepository.RemoveBrand(Id);
        }

        public SaveBrandRes SaveProductRes(SaveBrandReq brand)
        {
            return _brandRepository.Created(brand);
        }
    }
}
