using Dapper;
using SSH.DAL.Interface;
using SSH.Domain.Request.Category;
using SSH.Domain.Response.Brand;
using SSH.Domain.Response.Category;
using SSH.Domain.Response.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SSH.DAL
{
    public class CategoryRepository : BaseRepository,ICategoryRepository
    {
        public SaveCategoryRes Created(SaveCategoryReq category)
        {
            var result = new SaveCategoryRes()
            {
                Result = 0,
                Message = $"Đã xảy ra lỗi, vui lòng thử lại sau."
            };

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(@"ID", category.ID);
                parameters.Add(@"CategoryName", category.CategoryName);
                var response = SqlMapper.ExecuteScalar<int>(con, "proc_SaveCategory", param: parameters, commandType: CommandType.StoredProcedure);
                result.Result = response;
                result.Message = category.ID == 0 ?
                        $"Brand created success." :
                        $"Update brand success!";
                return result;
            }
            catch
            {
                return result;

            }
        }

        public IEnumerable<Brand> GetAllBrandsForCategory(int categoryId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(@"ID", categoryId);
            var brands = SqlMapper.Query<Brand>(con, "proc_GetBrandByCategory", parameters, commandType: CommandType.StoredProcedure).ToList();
            return brands;
        }

        public IEnumerable<Product> GetAllProductsForCategory(int categoryId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(@"ID", categoryId);
            var products = SqlMapper.Query<Product>(con, "proc_GetProductByCategory", parameters, commandType: CommandType.StoredProcedure).ToList();
            return products;
        }

        public Category GetCategoryByID(int Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(@"ID", Id);
            Category category = SqlMapper.Query<Category>(con, "proc_GetCategoryByID", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return category;
        }

        public IEnumerable<Category> GetData()
        {
            var categories = SqlMapper.Query<Category>(con, "proc_GetDataCategory", commandType: CommandType.StoredProcedure).ToList();
            return categories;
        }

        public IList<Category> ListCategory()
        {
            IList<Category> listCategory = SqlMapper.Query<Category>(con, "ListCategory", commandType: CommandType.StoredProcedure).ToList();
            return listCategory;
        }

        public bool RemoveCategory(int Id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(@"ID", Id);
                var result = SqlMapper.ExecuteScalar<bool>(con, "proc_DeleteCategoryID", param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
