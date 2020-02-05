using Dapper;
using SSH.DAL.Interface;
using SSH.Domain.Request.Brand;
using SSH.Domain.Response.Brand;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SSH.DAL
{
    public class BrandRepository : BaseRepository, IBrandRepository
    {
        public SaveBrandRes Created(SaveBrandReq brand)
        {
            var result = new SaveBrandRes()
            {
                Result = 0,
                Message = $"Đã xảy ra lỗi, vui lòng thử lại sau."
            };

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(@"ID", brand.ID);
                parameters.Add(@"BrandName", brand.BrandName);
                parameters.Add(@"ParenID", brand.ParenID);
                var response = SqlMapper.ExecuteScalar<int>(con, "proc_SaveBrand", param: parameters, commandType: CommandType.StoredProcedure);
                result.Result = response;
                result.Message = brand.ID == 0 ?
                        $"Brand created success." :
                        $"Update brand success!";
                return result;
            }
            catch
            {
                return result;

            }
        }

        public Brand GetBrandByID(int Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(@"ID", Id);
            Brand brand = SqlMapper.Query<Brand>(con, "proc_GetDataBrandID", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return brand;
        }

        public IEnumerable<Brand> GetData()
        {
            var brands = SqlMapper.Query<Brand>(con, "proc_GetDataBrand", commandType: CommandType.StoredProcedure).ToList();
            return brands;
        }

        public IList<Brand> ListBrand()
        {
            var brands = SqlMapper.Query<Brand>(con, "ListBrand", commandType: CommandType.StoredProcedure).ToList();
            return brands;
        }

        public bool RemoveBrand(int Id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(@"ID", Id);
                var result = SqlMapper.ExecuteScalar<bool>(con, "proc_DeleteBrandID", param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
