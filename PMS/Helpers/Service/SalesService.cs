using Dapper;
using PMS.Application.Request.Sales;
using PMS.Application.Request.Sales.Command;
using PMS.Context;
using PMS.Helpers.Interface;
using System.Data;
using System.Security.Claims;

namespace PMS.Helpers.Service
{
    public class SalesService : ISalesService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;

        //string Id = "";
        public SalesService(DapperContext dapperContext, ICurrentUserService currentUserService)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;

            /*Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);*/
        }
        public async Task<Result> MedicineSales(AddNewSales request)
        {
            try
            {
                if (request.SalesDetails.Count == 0)
                {
                    return Result.Failure(new List<string> { "Please Sales Minimum One Medicine " });
                }

                using (var context = _dapperContext.CreateConnection())
                {
                    string query = "SP_SalesInfo";
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);

                    parameter.Add("@CustomerName", request.CustomerName, DbType.String, ParameterDirection.Input);
                    parameter.Add("@ContactNo", request.ContactNo, DbType.String, ParameterDirection.Input);

                    parameter.Add("@TotalTaka", request.TotalTaka, DbType.Decimal, ParameterDirection.Input);
                    parameter.Add("@DiscountPercentage", request.DiscountPercentage, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@DiscountTaka", request.DiscountTaka, DbType.Decimal, ParameterDirection.Input);
                    parameter.Add("@SubTotal", request.SubTotal, DbType.Decimal, ParameterDirection.Input);
                    parameter.Add("@GrandTotal", request.GrandTotal, DbType.Decimal, ParameterDirection.Input);

                    parameter.Add("@ClientId", _currentUserService.UserId, DbType.Int32, ParameterDirection.Input);

                    parameter.Add("@CreateBy", _currentUserService.UserId.ToString(), DbType.String, ParameterDirection.Input);
                    parameter.Add("@MESSAGE", 0, DbType.Int32, ParameterDirection.Output);
                    var result = await context.ExecuteAsync(query, parameter);
                    int res = parameter.Get<int>("@MESSAGE");

                    if (res > 0)
                    {
                        foreach (var item in request.SalesDetails)
                        {
                            string queryDetails = "SP_SalesDetails";
                            DynamicParameters parameterForDetails = new DynamicParameters();
                            parameterForDetails.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@ClientId", _currentUserService.UserId, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@SalesInfoId", res, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@MedicineId", item.MedicineId, DbType.Int32, ParameterDirection.Input);

                            parameterForDetails.Add("@SalesPrice", item.SalesPrice, DbType.Decimal, ParameterDirection.Input);
                            parameterForDetails.Add("@Quantity", item.Quantity, DbType.Int32, ParameterDirection.Input);

                            parameterForDetails.Add("@TotalTaka", item.TotalTaka, DbType.Decimal, ParameterDirection.Input);
                            parameterForDetails.Add("@DiscountPercentage", item.DiscountPercentage, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@DiscountTaka", item.DiscountTaka, DbType.Decimal, ParameterDirection.Input);
                            parameterForDetails.Add("@SubTotal", item.SubTotal, DbType.Decimal, ParameterDirection.Input);
                            parameterForDetails.Add("@GrandTotal", item.GrandTotal, DbType.Decimal, ParameterDirection.Input);

                            parameterForDetails.Add("@CreateBy", _currentUserService.UserId.ToString(), DbType.String, ParameterDirection.Input);
                            parameterForDetails.Add("@Message", 0, DbType.Int32, ParameterDirection.Output);

                            await context.ExecuteAsync(queryDetails, parameterForDetails);
                        }
                        return Result.Success("Medicine Sales Successfully");
                    }
                    else
                    {
                        return Result.Failure(new List<string> { "Medicine Sales Failed " });
                    }
                }

            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message });
            }
        }
    }
}
