using Microsoft.AspNetCore.Mvc;
using SSH.BAL.Interface;
using SSH.Domain.Request.Brand;
using SSH.Domain.Response.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSH.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [Route("DataBrand")]
        public IEnumerable<Brand> DataBrand()
        {
            return _brandService.GetData();
        }

        [HttpGet]
        [Route("Brand/{Id}")]
        public Brand GetBrandByID(int Id)
        {
            return _brandService.GetBrandByID(Id);
        }

        [HttpPost]
        [Route("Created")]
        public SaveBrandRes Post([FromBody] SaveBrandReq model)
        {
            return _brandService.SaveProductRes(model);
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public bool Delete(int Id)
        {
            return _brandService.RemoveBrand(Id);
        }

        [HttpGet]
        [Route("ListBrand")]
        public IEnumerable<Brand> ListBrand()
        {
            return _brandService.ListBrand();
        }
    }
}
