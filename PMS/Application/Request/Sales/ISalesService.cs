using PMS.Application.Common.Pagins;
using PMS.Application.Request.Sales.Command;
using PMS.Application.Request.Sales.Query;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales
{
    public interface ISalesService
    {
        Task<IEnumerable<GetClientWiseMedicineForSalesViewModel>> GetClientWiseMedicineForSales(GetClientWiseMedicineForSales request);
        Task<PagedList<GetSalesViewModel>> GetSales(GetSales request);
        Task<Result> MedicineSales(AddNewSales request);
    }
}
