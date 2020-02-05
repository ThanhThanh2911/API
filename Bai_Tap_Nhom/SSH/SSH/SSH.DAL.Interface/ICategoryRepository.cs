using SSH.Domain.Request.Category;
using SSH.Domain.Response.Brand;
using SSH.Domain.Response.Category;
using SSH.Domain.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.DAL.Interface
{
    public interface ICategoryRepository
    {
        SaveCategoryRes Created(SaveCategoryReq category);
        IEnumerable<Category> GetData();
        Category GetCategoryByID(int Id);
        bool RemoveCategory(int Id);
        IEnumerable<Product> GetAllProductsForCategory(int categoryId);
        IEnumerable<Brand> GetAllBrandsForCategory(int categoryId);
        IList<Category> ListCategory();
    }
}
