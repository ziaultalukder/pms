using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class TodayMonthlyAndYearlySalesReport : IRequest<TodayMonthlyAndYearlySalesReportViewModel>
    {
        
    }

    public class TodayMonthlyAndYearlySalesReportHandler : IRequestHandler<TodayMonthlyAndYearlySalesReport, TodayMonthlyAndYearlySalesReportViewModel>
    {
        private readonly ISalesService salesService;

        public TodayMonthlyAndYearlySalesReportHandler(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<TodayMonthlyAndYearlySalesReportViewModel> Handle(TodayMonthlyAndYearlySalesReport request, CancellationToken cancellationToken)
        {
            return await salesService.TodayMonthlyAndYearlySalesReport(request);
        }
    }
}
