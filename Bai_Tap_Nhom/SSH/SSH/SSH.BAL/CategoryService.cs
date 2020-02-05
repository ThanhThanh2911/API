using SSH.BAL.Interface;
using SSH.DAL.Interface;
using SSH.Domain.Request.Category;
using SSH.Domain.Response.Brand;
using SSH.Domain.Response.Category;
using SSH.Domain.Response.Product;
using System.Collections.Generic;

namespace SSH.BAL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public SaveCategoryRes Created(SaveCategoryReq category)
        {
            return _categoryRepository.Created(category);
        }

        public IEnumerable<Brand> GetAllBrandsForCategory(int categoryId)
        {
            return _categoryRepository.GetAllBrandsForCategory(categoryId);
        }

        public IEnumerable<Product> GetAllProductsForCategory(int categoryId)
        {
            return _categoryRepository.GetAllProductsForCategory(categoryId);
        }

        public Category GetCategoryByID(int Id)
        {
            return _categoryRepository.GetCategoryByID(Id);
        }

        public IEnumerable<Category> GetData()
        {
            return _categoryRepository.GetData();
        }

        public IList<Category> ListCategory()
        {
            return _categoryRepository.ListCategory();
        }

        public bool RemoveCategory(int Id)
        {
            return _categoryRepository.RemoveCategory(Id);
        }
    }
}
