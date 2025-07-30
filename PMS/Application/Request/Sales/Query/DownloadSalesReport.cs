using MediatR;
using PMS.Application.Common.Pagins;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class DownloadSalesReport : IRequest<IEnumerable<DownloadSalesReportViewModel>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public DownloadSalesReport(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class DownloadSalesReportHandler : IRequestHandler<DownloadSalesReport, IEnumerable<DownloadSalesReportViewModel>>
    {
        private readonly ISalesService salesService;

        public DownloadSalesReportHandler(ISalesService _salesService)
        {
            salesService = _salesService;
        }
        public async Task<IEnumerable<DownloadSalesReportViewModel>> Handle(DownloadSalesReport request, CancellationToken cancellationToken)
        {
            return await salesService.DownloadSalesReport(request);
        }
    }
}
