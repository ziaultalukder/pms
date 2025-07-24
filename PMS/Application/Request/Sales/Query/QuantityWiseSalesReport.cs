using MediatR;
using PMS.Application.Common.Pagins;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class QuantityWiseSalesReport : IRequest<IEnumerable<QuantityWiseSalesReportViewModel>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public QuantityWiseSalesReport(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class QuantityWiseSalesReportHandler : IRequestHandler<QuantityWiseSalesReport, IEnumerable<QuantityWiseSalesReportViewModel>>
    {
        private readonly ISalesService salesService;

        public QuantityWiseSalesReportHandler(ISalesService _salesService)
        {
            salesService = _salesService;
        }
        public async Task<IEnumerable<QuantityWiseSalesReportViewModel>> Handle(QuantityWiseSalesReport request, CancellationToken cancellationToken)
        {
            return await salesService.QuantityWiseSalesReport(request);
        }
    }
}
