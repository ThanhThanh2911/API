using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SSH.Web.Models.Brand;

namespace SSH.Web.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllBrandsForCategory(int id)
        {
            var products = new List<BrandItem>();
            var url = $"{Common.Common.ApiUrl}/Categogy/GetAllBrandsForCategory/{id}";
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
                products = JsonConvert.DeserializeObject<List<BrandItem>>(responseData);
            }
            return Json(new { response = products, status = 1 });
        }


        public IActionResult DeleteBrand(int id)
        {
            try
            {
                var url = $"{Common.Common.ApiUrl}/Brand/Delete/{id}";
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

                    return new JsonResult(new
                    {
                        status = 1,
                        message = "Something went wrong, please contact administrator"
                    });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new
                {
                    status = -1,
                    message = "Something went wrong, please contact administrator"
                });
            }
        }

        [HttpPost]
        public IActionResult SaveBrand([FromBody] SaveBrand model)
        {
            var saveBrandResult = new BrandRes()
            {
                Message = "Not Found!",
                Result = 0
            };
            try
            {
                var url = $"{Common.Common.ApiUrl}/Brand/Created";
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
                    saveBrandResult = JsonConvert.DeserializeObject<BrandRes>(resKetQua);
                }
                if (saveBrandResult.Result > 0)
                {
                    return new JsonResult(new { status = 1, message = saveBrandResult.Message });
                }
            }
            catch (Exception ex)
            { }
            return new JsonResult(new { status = 0, message = saveBrandResult.Message });

        }

        //[HttpPost]
        //public IActionResult SuaNhanVien([FromBody] EditBrand model)
        //{
        //    try
        //    {
        //        var editResult = 0;
        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{Common.Common.ApiUrl}/nhanvien/suanhanvien");
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "PUT";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            var json = JsonConvert.SerializeObject(model);

        //            streamWriter.Write(json);
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            var result = streamReader.ReadToEnd();
        //            editResult = int.Parse(result);
        //        }

        //        if (editResult > 0)
        //        {
        //            return new JsonResult(new { status = 1, message = "Update Brand access" });
        //        }
        //    }catch(Exception ex) { }
        //}

        public IActionResult GetData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var getData = new List<BrandItem>();
                var url = $"{Common.Common.ApiUrl}/Brand/DataBrand";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
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
                    getData = JsonConvert.DeserializeObject<List<BrandItem>>(responseData);
                }
                var customerData = getData;

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var property = GetProperty(sortColumn);
                    if (sortColumnDirection == "asc")
                    {
                        customerData = customerData.OrderBy(property.GetValue).ToList();
                    }
                    else
                    {
                        customerData = customerData.OrderByDescending(property.GetValue).ToList();
                    }
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.BrandName.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //total number of rows count   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = -1,
                    message = "Something went wrong, please contact administrator."
                });
            }
        }

        [HttpGet]
        public IActionResult GetBrandById(int id)
        {
            var brand = new BrandItem();
            try
            {
                var url = $"{Common.Common.ApiUrl}/Brand/Brand/{id}";
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
                        ((IDisposable)responseStream)?.Dispose();
                    }

                    brand = JsonConvert.DeserializeObject<BrandItem>(responseData);
                }

                return Json(new { response = brand, status = 1 });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = -1,
                    response = brand
                });
            }
        }

        private PropertyInfo GetProperty(string columnName)
        {
            var properties = typeof(BrandItem).GetProperties();
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