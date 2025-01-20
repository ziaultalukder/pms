using Azure.Core;
using Dapper;
using PMS.Application.Request.Hospital;
using PMS.Application.Request.Hospital.Query;
using PMS.Context;
using PMS.Helpers.Interface;
using PMS.Models;
using PMS.ViewModel;
using System.Data;

namespace PMS.Helpers.Service
{
    public class HospitalService : IHospitalService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHostEnvironment _hostEnvironment;
        public HospitalService(DapperContext dapperContext, ICurrentUserService currentUserService, IHostEnvironment hostEnvironment)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IEnumerable<DivisionAndDistrictWiseHospitalViewModel>> GetDivisionAndDistrictWiseHospital(GetDivisionAndDistrictWiseHospital request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetDivisionAndDistrictWiseHospital";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@DivisionId", request.DivisionId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@DistrictId", request.DistrictId, DbType.Int32, ParameterDirection.Input);

                var result = await context.QueryAsync<DivisionAndDistrictWiseHospitalViewModel>(query, parameter);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<Models.Hospital>> GetHospitalById(GetHospitalById request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetHospitalById";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);

                var result = await context.QueryAsync<Models.Hospital>(query, parameter);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<PopularHospitalViewModel>> GetPopularHospitalInBangladesh()
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetPopularHospital";
                DynamicParameters parameter = new DynamicParameters();

                var result = await context.QueryAsync<PopularHospitalViewModel>(query);
                return result.ToList();
            }
        }
    }
}
