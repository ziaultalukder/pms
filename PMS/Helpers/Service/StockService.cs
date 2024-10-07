using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using PMS.Application.Request.Stock;
using PMS.Application.Request.Stock.Command;
using PMS.Context;
using PMS.Helpers.Interface;
using PMS.Models;
using System.Data;
using System.Security.Claims;

namespace PMS.Helpers.Service
{
    public class StockService : IStockService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;
        //string Id = "";
        public StockService(DapperContext dapperContext, ICurrentUserService currentUserService)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;

            //Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<Result> MedicineStock(AddNewStock request)
        {
            if (request.StockInDetails.Count == 0)
            {
                return Result.Failure(new List<string> { "Please Stock Minimum One Variant " });
            }
            else if (request.SupplierId == 0)
            {
                return Result.Failure(new List<string> { "Select Supplier " });
            }
            using (var context = _dapperContext.CreateConnection())
            {
                context.Open();
                using (var transactionScope = context.BeginTransaction())
                {
                    try
                    {
                        string query = "StockIn";
                        DynamicParameters parameter = new DynamicParameters();
                        parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@StockDate", request.StockDate, DbType.DateTime, ParameterDirection.Input);
                        parameter.Add("@SupplierId", request.SupplierId, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@TotalPrice", request.TotalPrice, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@DiscountPercentage", request.DiscountPercentage, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@DiscountTaka", request.DiscountTaka, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@DiscountValue", request.DiscountValue, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@isActive", request.IsActive, DbType.String, ParameterDirection.Input);
                        parameter.Add("@CreateBy", _currentUserService.UserId.ToString(), DbType.String, ParameterDirection.Input);
                        parameter.Add("@P_MESSAGE", 0, DbType.Int32, ParameterDirection.Output);
                        var result = await context.ExecuteAsync(query, parameter, transaction: transactionScope);
                        int res = parameter.Get<int>("@P_MESSAGE");

                        if (res > 0)
                        {
                            foreach (var item in request.StockInDetails)
                            {
                                string queryDetails = "StockInDetails";
                                DynamicParameters parameterForDetails = new DynamicParameters();
                                parameterForDetails.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                                parameterForDetails.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                                parameterForDetails.Add("@StockInfoId", res, DbType.Int32, ParameterDirection.Input);
                                parameterForDetails.Add("@MedicineId", item.MedicineId, DbType.String, ParameterDirection.Input);
                                parameterForDetails.Add("@NewQty", item.NewQty, DbType.Decimal, ParameterDirection.Input);
                                parameterForDetails.Add("@SalesPrice", item.SalesPrice, DbType.Int32, ParameterDirection.Input);
                                parameterForDetails.Add("@PruchasePrice", item.PruchasePrice, DbType.Decimal, ParameterDirection.Input);
                                parameterForDetails.Add("@CreateBy", _currentUserService.UserId.ToString(), DbType.String, ParameterDirection.Input);
                                await context.ExecuteAsync(queryDetails, parameterForDetails, transaction: transactionScope);
                            }

                            transactionScope.Commit();
                            return Result.Success("Medicine Stock Successfully");
                        }
                        else
                        {
                            transactionScope.Rollback();
                            return Result.Failure(new List<string> { "Medicine Stock Failed " });
                        }
                    }
                    catch (Exception ex)
                    {
                        transactionScope.Rollback();
                        return Result.Failure(new List<string> { ex.Message });
                    }
                }
            }
        }
    }
}
