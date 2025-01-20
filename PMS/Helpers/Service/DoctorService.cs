using Azure.Core;
using Dapper;
using PMS.Application.Request.Doctor;
using PMS.Application.Request.Doctor.Command;
using PMS.Application.Request.Doctor.Query;
using PMS.Context;
using PMS.Domain.Models;
using PMS.Helpers.Interface;
using PMS.Models;
using PMS.ViewModel;
using System.Data;

namespace PMS.Helpers.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHostEnvironment _hostEnvironment;
        public DoctorService(DapperContext dapperContext, ICurrentUserService currentUserService, IHostEnvironment hostEnvironment)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;
            _hostEnvironment = hostEnvironment;
        }
        
        public async Task<Result> AddDoctor(AddDoctor request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                var qryparameter = new { ContactNo = request.ContactNo };
                string queryForCheckUserId = "select ContactNo from Doctors where ContactNo = @ContactNo";
                var client = await context.QueryFirstOrDefaultAsync<Doctor>(queryForCheckUserId, qryparameter);
                if (client != null)
                {
                    return Result.Failure(new List<string> { "ContactNo Already Exist" });
                }

                if (ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    //return Result.Success(sellerProfile.SellerImageUrl);
                }
                else
                {
                    var docotorImg = Helper.SaveSingleImage(request.Image, Constant.DOCTOR_IMAGE_PATH, _hostEnvironment);

                    if (!docotorImg.Result.Succeed)
                    {
                        return Result.Failure(new List<string> { "Doctor Image Not Saved!!!!" });
                    }
                    else
                    {
                        request.Image = docotorImg.Result.Message;
                    }
                }

                string query = "AddDoctor";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@CategoryId", request.CategoryId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                parameter.Add("@Image", request.Image, DbType.String, ParameterDirection.Input);
                parameter.Add("@ContactNo", request.ContactNo, DbType.String, ParameterDirection.Input);
                parameter.Add("@Email", request.Email, DbType.String, ParameterDirection.Input);
                parameter.Add("@SeoName", request.Name.Replace(" ", "-"), DbType.String, ParameterDirection.Input);

                parameter.Add("@Tags", request.Tags, DbType.String, ParameterDirection.Input);
                parameter.Add("@Description", request.Description, DbType.String, ParameterDirection.Input);
                parameter.Add("@VisitingHoure", request.VisitingHoure, DbType.String, ParameterDirection.Input);
                
                parameter.Add("@Chember", request.Chember, DbType.String, ParameterDirection.Input);
                parameter.Add("@CreateBy", Convert.ToInt32(_currentUserService.UserId), DbType.Int32, ParameterDirection.Input);
                parameter.Add("@MESSAGE", "", DbType.Int32, ParameterDirection.Output);

                var result = await context.ExecuteAsync(query, parameter);
                int res = parameter.Get<int>("@MESSAGE");

                return Result.Success("Save Success");
            }
        }
        public async Task<IEnumerable<GetDoctorByCategoryIdViewModel>> GetDoctorCategoryId(GetDoctorCategoryId request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetDoctorByCategoryId";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@CategoryId", request.CategoryId, DbType.Int32, ParameterDirection.Input);

                var result = await context.QueryAsync<GetDoctorByCategoryIdViewModel>(query, parameter);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<DoctorsDegree>> GetDoctorDegree()
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "select Id, Name from Degree";
                var result = await context.QueryAsync<DoctorsDegree>(query);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<GetPopularDoctorViewModel>> GetPopularDoctor()
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetPopularDoctor";
                DynamicParameters parameter = new DynamicParameters();
                var result = await context.QueryAsync<GetPopularDoctorViewModel>(query);
                return result.ToList();
            }
        }
        public async Task<Result> UpdateDoctor(UpdateDoctor request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                if (ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    //return Result.Success(sellerProfile.SellerImageUrl);
                }
                else
                {
                    var docotorImg = Helper.SaveSingleImage(request.Image, Constant.DOCTOR_IMAGE_PATH, _hostEnvironment);

                    if (!docotorImg.Result.Succeed)
                    {
                        return Result.Failure(new List<string> { "Doctor Image Not Saved!!!!" });
                    }
                    else
                    {
                        request.Image = docotorImg.Result.Message;
                    }
                }

                string query = "UpdateDoctor";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@CategoryId", request.CategoryId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                parameter.Add("@Image", request.Image, DbType.String, ParameterDirection.Input);
                parameter.Add("@ContactNo", request.ContactNo, DbType.String, ParameterDirection.Input);
                parameter.Add("@Email", request.Email, DbType.String, ParameterDirection.Input);
                parameter.Add("@SeoName", request.Name.Replace(" ", "-"), DbType.String, ParameterDirection.Input);

                parameter.Add("@Description", request.Description, DbType.String, ParameterDirection.Input);
                parameter.Add("@VisitingHoure", request.VisitingHoure, DbType.String, ParameterDirection.Input);

                parameter.Add("@Chember", request.Chember, DbType.String, ParameterDirection.Input);
                parameter.Add("@CreateBy", Convert.ToInt32(_currentUserService.UserId), DbType.Int32, ParameterDirection.Input);
                parameter.Add("@MESSAGE", "", DbType.Int32, ParameterDirection.Output);

                var result = await context.ExecuteAsync(query, parameter);
                int res = parameter.Get<int>("@MESSAGE");

                return Result.Success("Update Success");
            }
        }
    }
}
