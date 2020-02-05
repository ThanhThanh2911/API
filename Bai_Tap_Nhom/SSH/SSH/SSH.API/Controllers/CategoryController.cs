using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSH.BAL.Interface;
using SSH.Domain.Request.Category;
using SSH.Domain.Response.Brand;
using SSH.Domain.Response.Category;
using SSH.Domain.Response.Product;

namespace SSH.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("DataCategory")]
        public IEnumerable<Category> DataCategory()
        {
            return _categoryService.GetData();
        }

        [HttpGet]
        [Route("GetAllProductsForCategory/{Id}")]
        public IEnumerable<Product> GetAllProductsForCategory(int Id)
        {
            return _categoryService.GetAllProductsForCategory(Id);
        }

        [HttpGet]
        [Route("GetAllBrandsForCategory/{Id}")]
        public IEnumerable<Brand> GetAllBrandsForCategory(int Id)
        {
            return _categoryService.GetAllBrandsForCategory(Id);
        }

        [HttpGet]
        [Route("Category/{Id}")]
        public Category GetCategoryByID(int Id)
        {
            return _categoryService.GetCategoryByID(Id);
        }

        [HttpPost]
        [Route("Created")]
        public SaveCategoryRes Post([FromBody] SaveCategoryReq model)
        {
            return _categoryService.Created(model);
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public bool Delete(int Id)
        {
            return _categoryService.RemoveCategory(Id);
        }

        [HttpGet]
        [Route("ListCategory")]
        public IEnumerable<Category> ListCategory()
        {
            return _categoryService.ListCategory();
        }
    }
}