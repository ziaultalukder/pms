using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Configuration.Query;
using PMS.Application.Request.Configuration;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class GetClientWiseMedicineForSales : IRequest<IEnumerable<GetClientWiseMedicineForSalesViewModel>>
    {
        public string MedicineName { get; set; }
        public GetClientWiseMedicineForSales(string medicineName)
        {
            MedicineName = medicineName;
        }
    }

    public class GetClientWiseMedicineForSalesHandler : IRequestHandler<GetClientWiseMedicineForSales, IEnumerable<GetClientWiseMedicineForSalesViewModel>>
    {
        private readonly ISalesService salesService;

        public GetClientWiseMedicineForSalesHandler(ISalesService _salesService)
        {
            salesService = _salesService;
        }
        public async Task<IEnumerable<GetClientWiseMedicineForSalesViewModel>> Handle(GetClientWiseMedicineForSales request, CancellationToken cancellationToken)
        {
            return await salesService.GetClientWiseMedicineForSales(request);
        }
    }
}
