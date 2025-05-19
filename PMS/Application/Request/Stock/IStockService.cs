using PMS.Application.Common.Pagins;
using PMS.Application.Request.Stock.Command;
using PMS.Application.Request.Stock.Query;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Stock
{
    public interface IStockService
    {
        Task<PagedList<GetStockViewModel>> GetStocks(GetStock request);
        Task<Result> MedicineStock(AddNewStock request);
    }
}
