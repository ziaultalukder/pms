using PMS.Application.Common.Pagins;
using PMS.Application.Request.Sales.Command;
using PMS.Application.Request.Sales.Query;
using PMS.Application.Request.Stock.Query;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales
{
    public interface ISalesService
    {
        Task<IEnumerable<GetClientWiseMedicineForSalesViewModel>> GetClientWiseMedicineForSales(GetClientWiseMedicineForSales request);
        Task<GetSalesByInvoiceNo> GetSaleByInvoice(GetSalesInfoForRefund request);
        Task<PagedList<GetSalesViewModel>> GetSales(GetSales request);
        Task<GetSalesByIdViewModel> GetSalesDetailsById(GetSalesDetailsById request);
        Task<Result> MedicineSales(AddNewSales request);
        Task<Result> SalesRefund(SalesRefund request);
        Task<PagedList<SalesReportViewModel>> SalesReport(SalesReport request);
        Task<TodayMonthlyAndYearlySalesReportViewModel> TodayMonthlyAndYearlySalesReport(TodayMonthlyAndYearlySalesReport request);
        Task<IEnumerable<WeeklyChartSalesReportViewModel>> WeeklyChartSalesReport(WeeklyChartSalesReport request);
        Task<IEnumerable<WeeklyTopSalesMedicineReportViewModel>> WeeklyTopSalesMedicineReport(WeeklyTopSalesMedicineReport request);
    }
}
