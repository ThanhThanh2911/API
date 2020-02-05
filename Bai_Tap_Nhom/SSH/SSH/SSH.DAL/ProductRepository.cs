using Dapper;
using SSH.DAL.Interface;
using SSH.Domain.Request.Product;
using SSH.Domain.Response.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SSH.DAL
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public bool DeleteProduct(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(@"ID", id);
                var result = SqlMapper.ExecuteScalar<bool>(con, "proc_DeleteProductById", param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = SqlMapper.Query<Product>(con, "proc_GetProducts", commandType: CommandType.StoredProcedure).ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(@"ID", id);
            Product product = SqlMapper.Query<Product>(con, "proc_GetProductById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return product;
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(@"ProductName", name);
            var product = SqlMapper.Query<Product>(con, "proc_GetProductByName", parameters, commandType: CommandType.StoredProcedure).ToList();
            return product;
        }

        public SaveProductRes SaveProduct(SaveProductReq saveProductReq)
        {
            var result = new SaveProductRes()
            {
                Result = 0,
                Message = $"Đã xảy ra lỗi, vui lòng thử lại sau."
            };

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(@"ID", saveProductReq.ID);
                parameters.Add(@"ProductName", saveProductReq.ProductName);
                parameters.Add(@"BrandID", saveProductReq.BrandID);
                parameters.Add(@"CategoryID", saveProductReq.CategoryID);
                parameters.Add(@"Price", saveProductReq.Price);
                var response = SqlMapper.ExecuteScalar<int>(con, "proc_SaveProduct", param: parameters, commandType: CommandType.StoredProcedure);
                result.Result = response;
                result.Message = saveProductReq.ID != 0 ?
                        $"Sản phẩm đã được tạo thành công." :
                        $"Sản phẩm đã được cập nhật thành công";
                return result;
            }
            catch (Exception ex)
            {
                return result;

            }
        }
    }
}
