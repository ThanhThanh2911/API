using SSH.Domain.Request.Product;
using SSH.Domain.Response.Product;
using System;
using System.Collections.Generic;

namespace SSH.DAL.Interface
{
    public interface IProductRepository
    {
        SaveProductRes SaveProduct(SaveProductReq saveProductReq);
        Product GetProductById(int id);
        bool DeleteProduct(int id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByName(string name);
    }
}
