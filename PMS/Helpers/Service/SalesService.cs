﻿using Dapper;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Sales;
using PMS.Application.Request.Sales.Command;
using PMS.Application.Request.Sales.Query;
using PMS.Context;
using PMS.Helpers.Interface;
using PMS.ViewModel;
using System.Data;

namespace PMS.Helpers.Service
{
    public class SalesService : ISalesService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;

        public SalesService(DapperContext dapperContext, ICurrentUserService currentUserService)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;

            /*Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);*/
        }

        public async Task<IEnumerable<DownloadSalesReportViewModel>> DownloadSalesReport(DownloadSalesReport request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "DownloadSalesReport";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@StartDate", request.StartDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@EndDate", request.EndDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryAsync<DownloadSalesReportViewModel>(query, parameter);

                return result.ToList();
            }
        }

        public async Task<IEnumerable<GetClientWiseMedicineForSalesViewModel>> GetClientWiseMedicineForSales(GetClientWiseMedicineForSales request)
       {
            using (var context = _dapperContext.CreateConnection())
            {
                string conditionClause = " ";
                string query = "select * from GetClientWiseMedicineForSales_VW WHERE Quantity > 0 and ClientId =" + _currentUserService.ClientId;

                if (!string.IsNullOrEmpty(request.MedicineName))
                {
                    conditionClause = "AND ";
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " BrandName Like '" + request.MedicineName.Trim() + "%' ";
                }

                var clientWiseMedicine = await context.QueryAsync<GetClientWiseMedicineForSalesViewModel>(query);
                return clientWiseMedicine;
            }
        }

        public async Task<GetSalesByInvoiceNo> GetSaleByInvoice(GetSalesInfoForRefund request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetSalesInfoByInvoiceNo";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@InvoiceNo", request.InvoiceNo, DbType.String, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryFirstOrDefaultAsync<GetSalesByInvoiceNo>(query, parameter);
                GetSalesByInvoiceNo getSalesByInvoiceNo = result;
                if (result != null)
                {
                    string query1 = "GetSalesDetailsBySalesInfoId";
                    DynamicParameters parameter1 = new DynamicParameters();
                    parameter1.Add("@SalesInfoId", result.Id, DbType.String, ParameterDirection.Input);
                    var result1 = await context.QueryAsync<SalesDetailsViewModel>(query1, parameter1);
                    getSalesByInvoiceNo.SalesDetailsViewModels = result1;
                }
                return getSalesByInvoiceNo;
            }
        }

        public async Task<PagedList<GetSalesViewModel>> GetSales(GetSales request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetSales";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@StartDate", request.StartDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@EndDate", request.EndDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageNo", (request.CurrentPage - 1) * request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@ItemPerPage", request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryAsync<GetSalesViewModel>(query, parameter);
                return new PagedList<GetSalesViewModel>(result.ToList(), request.CurrentPage, request.ItemsPerPage, result.Count());
            }
        }

        public async Task<GetSalesByIdViewModel> GetSalesDetailsById(GetSalesDetailsById request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                GetSalesByIdViewModel getSalesByIdViewModel = new GetSalesByIdViewModel();
                
                var parameters = new { Id = request.Id, ClientId = _currentUserService.ClientId};
                var sql = "select Id,TotalTaka,DiscountPercentage,DiscountTaka,GrandTotal,InvoiceNo,IsRefundable,CreateDate from SalesInfo where Id = @Id AND ClientId = @ClientId";

                var SalesInfo = context.QueryFirstOrDefault<GetSalesByIdViewModel>(sql, parameters);

                var SalesDetailsQry = "select Id,SalesInfoId,SalesPrice,Quantity,RefundQty,GrandTotal, BrandName, ManufacturerName from SalesDetails inner join MedicineList on MedicineId = SL where SalesInfoId = @Id";

                var SalesDetails = context.Query<GetSalesDetailsByIdViewModel>(SalesDetailsQry, parameters);
                SalesInfo.SalesDetails = SalesDetails.ToList();

                return SalesInfo;
            }
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
                    context.Open();
                    using(var tran =  context.BeginTransaction())
                    {
                        string query = "SP_SalesInfo";
                        DynamicParameters parameter = new DynamicParameters();
                        parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);

                        parameter.Add("@CustomerName", request.CustomerName == string.Empty ? null : request.CustomerName, DbType.String, ParameterDirection.Input);
                        parameter.Add("@ContactNo", request.ContactNo == string.Empty ? null : request.ContactNo, DbType.String, ParameterDirection.Input);

                        parameter.Add("@TotalTaka", request.TotalTaka, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@DiscountPercentage", request.DiscountPercentage, DbType.Int32, ParameterDirection.Input);
                        parameter.Add("@DiscountTaka", request.DiscountTaka, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@SubTotal", request.SubTotal, DbType.Decimal, ParameterDirection.Input);
                        parameter.Add("@GrandTotal", request.GrandTotal, DbType.Decimal, ParameterDirection.Input);

                        parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);

                        parameter.Add("@CreateBy", _currentUserService.UserId.ToString(), DbType.String, ParameterDirection.Input);
                        parameter.Add("@MESSAGE", 0, DbType.Int32, ParameterDirection.Output);
                        var result = await context.ExecuteAsync(query, parameter, transaction: tran);
                        int res = parameter.Get<int>("@MESSAGE");

                        if (res > 0)
                        {
                            foreach (var item in request.SalesDetails)
                            {
                                string queryDetails = "SP_SalesDetails";
                                DynamicParameters parameterForDetails = new DynamicParameters();
                                parameterForDetails.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                                parameterForDetails.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
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

                                await context.ExecuteAsync(queryDetails, parameterForDetails, transaction:tran);
                            }
                        }
                        else
                        {
                            tran.Rollback();
                            return Result.Failure(new List<string> { "Medicine Sales Failed " });
                        }

                        tran.Commit();
                        return Result.Success("Medicine Sales Successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message });
            }
        }

        public async Task<IEnumerable<QuantityWiseSalesReportViewModel>> QuantityWiseSalesReport(QuantityWiseSalesReport request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "QuantityWiseSalesReport";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@StartDate", request.StartDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@EndDate", request.EndDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryAsync<QuantityWiseSalesReportViewModel>(query, parameter);
                return result.ToList();
            }
        }

        public async Task<Result> SalesRefund(SalesRefund request)
        {
            try
            {
                using (var context = _dapperContext.CreateConnection())
                {
                    context.Open();
                    using (var transaction = context.BeginTransaction())
                    {
                        string query = "InsertSalesRefund";
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
                            string queryForDetails = "InsertSalesRefundDetails";
                            DynamicParameters parameterForDetails = new DynamicParameters();
                            parameterForDetails.Add("@Id", item.Id, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@MedicineId", item.MedicineId, DbType.Int32, ParameterDirection.Input);

                            parameterForDetails.Add("@ExistingQty", item.SalesQty, DbType.Int32, ParameterDirection.Input);

                            parameterForDetails.Add("@RefundQty", item.RefundQty, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@TotalTaka", item.TotalTaka, DbType.Decimal, ParameterDirection.Input);

                            parameterForDetails.Add("@CreateBy", _currentUserService.UserId, DbType.Int32, ParameterDirection.Input);
                            parameterForDetails.Add("@MESSAGE", 0, DbType.Int32, ParameterDirection.Output);

                            await context.ExecuteAsync(queryForDetails, parameterForDetails, transaction: transaction);
                        }
                        transaction.Commit();
                        return Result.Success("Success");

                    }
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message});
            }
        }

        public async Task<PagedList<SalesReportViewModel>> SalesReport(SalesReport request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "SalesReport";
                DynamicParameters parameter = new DynamicParameters();
                
                parameter.Add("@StartDate", request.StartDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@EndDate", request.EndDate, DbType.String, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageNo", (request.CurrentPage - 1) * request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@ItemPerPage", request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@ItemPerPage", request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@TotalItem", 0, DbType.Int32, ParameterDirection.Output);
                var result = await context.QueryAsync<SalesReportViewModel>(query, parameter);
                int totalitems = parameter.Get<int>("@TotalItem");

                return new PagedList<SalesReportViewModel>(result.ToList(), request.CurrentPage, request.ItemsPerPage, totalitems);
            }
        }

        public async Task<TodayMonthlyAndYearlySalesReportViewModel> TodayMonthlyAndYearlySalesReport(TodayMonthlyAndYearlySalesReport request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "TodayMonthlyAndYearlySalesReport";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryFirstOrDefaultAsync<TodayMonthlyAndYearlySalesReportViewModel>(query, parameter);
                return result;
            }
        }

        public async Task<IEnumerable<WeeklyChartSalesReportViewModel>> WeeklyChartSalesReport(WeeklyChartSalesReport request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "WeeklyChartSalesReport";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryAsync<WeeklyChartSalesReportViewModel>(query, parameter);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<WeeklyTopSalesMedicineReportViewModel>> WeeklyTopSalesMedicineReport(WeeklyTopSalesMedicineReport request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "WeeklyTopSalesMedicineReport";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Value", request.Value, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                var result = await context.QueryAsync<WeeklyTopSalesMedicineReportViewModel>(query, parameter);
                return result.ToList();
            }
        }
    }
}
