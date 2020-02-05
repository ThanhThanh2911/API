using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SSH.Web.Models.Product;

namespace SSH.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            try
            {
                //LinQ
                //Lambda
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                // getting all Customer data  
                var products = new List<ProductView>();
                var url = $"{Common.Common.ApiUrl}/Product/GetProducts";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                var response = httpWebRequest.GetResponse();
                {
                    string responseData;
                    Stream responseStream = response.GetResponseStream();
                    try
                    {
                        StreamReader streamReader = new StreamReader(responseStream);
                        try
                        {
                            responseData = streamReader.ReadToEnd();
                        }
                        finally
                        {
                            ((IDisposable)streamReader).Dispose();
                        }
                    }
                    finally
                    {
                        ((IDisposable)responseStream).Dispose();
                    }
                    products = JsonConvert.DeserializeObject<List<ProductView>>(responseData);
                }
                var productData = products;

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var property = GetProperty(sortColumn);
                    if (sortColumnDirection == "asc")
                    {
                        productData = productData.OrderBy(property.GetValue).ToList();
                    }
                    else
                    {
                        productData = productData.OrderByDescending(property.GetValue).ToList();
                    }
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    productData = productData.Where(m => m.ProductName.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //total number of rows counts   
                recordsTotal = productData.Count();
                //Paging   
                var data = productData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = -1,
                    message = "Đã xảy ra lỗi, vui lòng thử lại sau."
                });
            }
        }

        [HttpPost]
        public IActionResult SaveProduct([FromBody] SaveProduct model)
        {
            var saveProductResult = new SaveProductRes()
            {
                Message = "Lỗi, vui lòng thử lại.",
                Result = 0
            };
            try
            {
                if (model != null)
                {
                    if (model.ID == 0)
                    {
                        var url = $"{Common.Common.ApiUrl}/Product/SaveProduct";
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        using (var streamWrite = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            var json = JsonConvert.SerializeObject(model);
                            streamWrite.Write(json);
                        }

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var resKetQua = streamReader.ReadToEnd();
                            saveProductResult = JsonConvert.DeserializeObject<SaveProductRes>(resKetQua);
                        }
                        if (saveProductResult.Result > 0)
                        {
                            return new JsonResult(new
                            {
                                status = 1,
                                message = saveProductResult.Message
                            });
                        }
                    }
                    else //update student by StudentId
                    {
                        var product = new SaveProduct();
                        var url = $"{Common.Common.ApiUrl}/Product/GetById/{product.ID}";
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.Method = "GET";
                        var response = httpWebRequest.GetResponse();
                        {
                            string responseData;
                            Stream responseStream = response.GetResponseStream();
                            try
                            {
                                StreamReader streamReader = new StreamReader(responseStream);
                                try
                                {
                                    responseData = streamReader.ReadToEnd();
                                }
                                finally
                                {
                                    ((IDisposable)streamReader).Dispose();
                                }
                            }
                            finally
                            {
                                ((IDisposable)responseStream).Dispose();
                            }
                            product = JsonConvert.DeserializeObject<SaveProduct>(responseData);
                        }
                        product = model;
                        url = $"{Common.Common.ApiUrl}/Product/SaveProduct";
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        using (var streamWrite = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            var json = JsonConvert.SerializeObject(model);
                            streamWrite.Write(json);
                        }

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var resKetQua = streamReader.ReadToEnd();
                            saveProductResult = JsonConvert.DeserializeObject<SaveProductRes>(resKetQua);
                        }
                        if (saveProductResult.Result > 0)
                        {
                            return new JsonResult(new
                            {
                                status = 1,
                                message = saveProductResult.Message
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new JsonResult(new
            {
                status = 0,
                message = saveProductResult.Message
            });
        }

        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            var product = new ProductView();
            try
            {
                var url = $"{Common.Common.ApiUrl}/Product/GetById/{id}";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                var response = httpWebRequest.GetResponse();
                {
                    string responseData;
                    Stream responseStream = response.GetResponseStream();
                    try
                    {
                        StreamReader streamReader = new StreamReader(responseStream);
                        try
                        {
                            responseData = streamReader.ReadToEnd();
                        }
                        finally
                        {
                            ((IDisposable)streamReader).Dispose();
                        }
                    }
                    finally
                    {
                        ((IDisposable)responseStream).Dispose();
                    }
                    product = JsonConvert.DeserializeObject<ProductView>(responseData);
                    return new JsonResult(new
                    {
                        status = 1,
                        response = product
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = -1,
                    response = product
                });
            }

        }

        public IActionResult DeleteProduct(int id)
        {
            var product = false;
            try
            {
                var url = $"{Common.Common.ApiUrl}/Product/DeleteProduct/{id}";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "DELETE";
                var response = httpWebRequest.GetResponse();
                {
                    string responseData;
                    Stream responseStream = response.GetResponseStream();
                    try
                    {
                        StreamReader streamReader = new StreamReader(responseStream);
                        try
                        {
                            responseData = streamReader.ReadToEnd();
                        }
                        finally
                        {
                            ((IDisposable)streamReader).Dispose();
                        }
                    }
                    finally
                    {
                        ((IDisposable)responseStream).Dispose();
                    }
                    product = JsonConvert.DeserializeObject<bool>(responseData);
                    return new JsonResult(new
                    {
                        status = 1,
                        response = product
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return new JsonResult(new
            {
                status = -1,
                message = "Something went wrong, please contact administrator."
            });
        }
        /// <summary>
        /// get list category
        /// </summary>
        /// <returns></returns>
        public IActionResult ListCategory()
        {
            var categoryItems = new List<CategoryItem>();
            try
            {
                var url = $"{Common.Common.ApiUrl}/Category/ListCategory";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                var response = httpWebRequest.GetResponse();
                {
                    string responseData;
                    Stream responseStream = response.GetResponseStream();
                    try
                    {
                        StreamReader streamReader = new StreamReader(responseStream);
                        try
                        {
                            responseData = streamReader.ReadToEnd();
                        }
                        finally
                        {
                            ((IDisposable)streamReader).Dispose();
                        }
                    }
                    finally
                    {
                        ((IDisposable)responseStream).Dispose();
                    }
                    categoryItems = JsonConvert.DeserializeObject<List<CategoryItem>>(responseData);
                }
                return new JsonResult(new
                {
                    status = 1,
                    response = categoryItems
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { response = categoryItems, status = -1 });
            }

        }

        public IActionResult ListBrand()
        {
            var brandItems = new List<BrandItem>();
            try
            {
                var url = $"{Common.Common.ApiUrl}/Brand/ListBrand";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                var response = httpWebRequest.GetResponse();
                {
                    string responseData;
                    Stream responseStream = response.GetResponseStream();
                    try
                    {
                        StreamReader streamReader = new StreamReader(responseStream);
                        try
                        {
                            responseData = streamReader.ReadToEnd();
                        }
                        finally
                        {
                            ((IDisposable)streamReader).Dispose();
                        }
                    }
                    finally
                    {
                        ((IDisposable)responseStream).Dispose();
                    }
                    brandItems = JsonConvert.DeserializeObject<List<BrandItem>>(responseData);
                }
                return new JsonResult(new
                {
                    status = 1,
                    response = brandItems
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { response = brandItems, status = -1 });
            }

        }

        private PropertyInfo GetProperty(string columnName)
        {
            var properties = typeof(ProductView).GetProperties();
            PropertyInfo prop = null;
            foreach (var property in properties)
            {
                if (property.Name.ToLower().Equals(columnName.ToLower()))
                {
                    prop = property;
                    break;
                }
            }
            return prop;
        }
    }
}