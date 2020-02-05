using SSH.Domain.Request.Product;
using SSH.Domain.Response.Product;
using System.Collections.Generic;

namespace SSH.BAL.Interface
{
    public interface IProductService
    {
        SaveProductRes SaveProductRes(SaveProductReq saveProductReq);
        Product GetProductById(int id);
        bool DeleteProduct(int id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByName(string name);
    }
}
