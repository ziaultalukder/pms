using PMS.Application.Request.Stock.Command;
using PMS.Helpers;

namespace PMS.Application.Request.Stock
{
    public interface IStockService
    {
        Task<Result> MedicineStock(AddNewStock request);
    }
}
