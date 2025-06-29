using MediatR;
using PMS.Application.Common.Pagins;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class SalesReport : PageParameters, IRequest<PagedList<SalesReportViewModel>> //IRequest<IEnumerable<GetSalesViewModel>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public SalesReport(string startDate, string endDate, int currentPage, int itemsPerpage) : base(currentPage, itemsPerpage)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class SalesReportHandler : IRequestHandler<SalesReport, PagedList<SalesReportViewModel>>
    {
        private readonly ISalesService salesService;

        public SalesReportHandler(ISalesService _salesService)
        {
            salesService = _salesService;
        }
        public async Task<PagedList<SalesReportViewModel>> Handle(SalesReport request, CancellationToken cancellationToken)
        {
            return await salesService.SalesReport(request);
        }
    }
}
