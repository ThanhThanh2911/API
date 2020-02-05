using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSH.BAL.Interface;
using SSH.Domain.Request.Product;
using SSH.Domain.Response.Product;

namespace SSH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [Route("SaveProduct")]
        public SaveProductRes SaveProduct([FromBody] SaveProductReq request)
        {
            return productService.SaveProductRes(request);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public Product GetProductById(int id)
        {
            return productService.GetProductById(id);
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public bool DeleteProduct(int id)
        {
            return productService.DeleteProduct(id);
        }

        [HttpGet]
        [Route("GetProducts")]
        public IEnumerable<Product> GetAllProducts()
        {
            return productService.GetAllProducts();
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IEnumerable<Product> GetProductByName(string name)
        {
            return productService.GetProductByName(name);
        }
    }
}