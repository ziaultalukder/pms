using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Models;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class GetSales : PageParameters, IRequest<PagedList<GetSalesViewModel>> //IRequest<IEnumerable<GetSalesViewModel>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public GetSales(string startDate, string endDate, int currentPage, int itemsPerpage) : base(currentPage, itemsPerpage) 
        { 
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class GetSalesHandler : IRequestHandler<GetSales, PagedList<GetSalesViewModel>>
    {
        private readonly ISalesService salesService;

        public GetSalesHandler(ISalesService _salesService)
        {
            salesService = _salesService;
        }
        public async Task<PagedList<GetSalesViewModel>> Handle(GetSales request, CancellationToken cancellationToken)
        {
            return await salesService.GetSales(request);
        }
    }

}
