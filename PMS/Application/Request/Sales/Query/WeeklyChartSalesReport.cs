using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class WeeklyChartSalesReport : IRequest<IEnumerable<WeeklyChartSalesReportViewModel>>
    {

    }

    public class WeeklyChartSalesReportHandler : IRequestHandler<WeeklyChartSalesReport, IEnumerable<WeeklyChartSalesReportViewModel>>
    {
        private readonly ISalesService salesService;

        public WeeklyChartSalesReportHandler(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<IEnumerable<WeeklyChartSalesReportViewModel>> Handle(WeeklyChartSalesReport request, CancellationToken cancellationToken)
        {
            return await salesService.WeeklyChartSalesReport(request);
        }
    }
}
