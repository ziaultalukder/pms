using Dapper;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Stock;
using PMS.Application.Request.Stock.Command;
using PMS.Application.Request.Stock.Query;
using PMS.Context;
using PMS.Helpers.Interface;
using PMS.ViewModel;
using System.Data;

namespace PMS.Helpers.Service
{
    public class StockService : IStockService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;
        public StockService(DapperContext dapperContext, ICurrentUserService currentUserService)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;

            //Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<GetStockByInvoiceNo> GetStockByInvoiceNo(GetStockInfoForRefund request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                var parameters = new { InvoiceNo = request.InvoiceNo };
                string query = "select * from stock_info where Invoice =@InvoiceNo ";
                var result = await context.QueryFirstOrDefaultAsync<GetStockByInvoiceNo>(query, parameters);

                if (result != null)
                {
                    string query1 = "select Id,MedicineId,BrandName,NewQty,SalesPrice, PruchasePrice from stock_in_details Innser join MedicineList on MedicineId = SL where StockInfoId =" + result.Id;
                    var result1 = await context.QueryAsync<GetStockDetailsViewModel>(query1);
                    result.StockDetailsViewModels= result1;
                }
                return result;
            }
        }
        public async Task<PagedList<GetStockViewModel>> GetStocks(GetStock request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetStock";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@StartDate", request.StartDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@EndDate", request.EndDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageNo", (request.CurrentPage - 1) * request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@ItemPerPage", request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryAsync<GetStockViewModel>(query, parameter);
                return new PagedList<GetStockViewModel>(result.ToList(), request.CurrentPage, request.ItemsPerPage, result.Count());
            }
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
                        parameter.Add("@TotalPrice", request.TotalPrice, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@DiscountPercentage", request.DiscountPercentage, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@DiscountTaka", request.DiscountTaka, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@DiscountValue", request.DiscountValue, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@isActive", request.IsActive, DbType.String, ParameterDirection.Input);
                        parameter.Add("@GrandTotal", request.GrandTotal, DbType.Decimal, ParameterDirection.Input);
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
                                parameterForDetails.Add("@SalesPrice", item.SalesPrice, DbType.Decimal, ParameterDirection.Input);
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
        public async Task<Result> StockRefund(StockRefund request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                context.Open();
                using (var transaction = context.BeginTransaction())
                {
                    string query = "InsertStockRefund";
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);

                    parameter.Add("@TotalTaka", request.TotalTaka, DbType.Decimal, ParameterDirection.Input);
                    parameter.Add("@Discount", request.Discount, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@DiscountTaka", request.DiscountTaka, DbType.Decimal, ParameterDirection.Input);
                    parameter.Add("@GrandTotal", request.GrandTotal, DbType.Decimal, ParameterDirection.Input);
                    parameter.Add("@CreateBy", _currentUserService.UserId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@MESSAGE", 0, DbType.Int32, ParameterDirection.Output);

                    await context.ExecuteAsync(query, parameter, transaction: transaction);

                    foreach (var item in request.RefundDetails)
                    {
                        string queryForDetails = "InsertStockRefundDetails";
                        DynamicParameters parameterForDetails = new DynamicParameters();
                        parameterForDetails.Add("@Id", item.Id, DbType.Int32, ParameterDirection.Input);
                        parameterForDetails.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                        parameterForDetails.Add("@MedicineId", item.MedicineId, DbType.Int32, ParameterDirection.Input);
                        parameterForDetails.Add("@RefundQty", item.RefundQty, DbType.Int32, ParameterDirection.Input);
                        parameterForDetails.Add("@ExistingQty", item.ExistingQty, DbType.Int32, ParameterDirection.Input);
                        parameterForDetails.Add("@CreateBy", _currentUserService.UserId, DbType.Int32, ParameterDirection.Input);
                        parameterForDetails.Add("@MESSAGE", 0, DbType.Int32, ParameterDirection.Output);

                        await context.ExecuteAsync(queryForDetails, parameterForDetails, transaction: transaction);
                    }
                    transaction.Commit();
                    return Result.Success("Success");
                }
            }
        }
    }
}
