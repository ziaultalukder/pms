using PMS.Application.Request.Sales.Command;
using PMS.Helpers;

namespace PMS.Application.Request.Sales
{
    public interface ISalesService
    {
        Task<Result> MedicineSales(AddNewSales request);
    }
}
