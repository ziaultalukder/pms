using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class WeeklyTopSalesMedicineReport : IRequest<IEnumerable<WeeklyTopSalesMedicineReportViewModel>>
    {
        public int Value { get; set; }
        public WeeklyTopSalesMedicineReport(int value)
        {
            Value = value;
        }
    }

    public class WeeklyTopSalesMedicineReportHandler : IRequestHandler<WeeklyTopSalesMedicineReport, IEnumerable<WeeklyTopSalesMedicineReportViewModel>>
    {
        private readonly ISalesService salesService;

        public WeeklyTopSalesMedicineReportHandler(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<IEnumerable<WeeklyTopSalesMedicineReportViewModel>> Handle(WeeklyTopSalesMedicineReport request, CancellationToken cancellationToken)
        {
            return await salesService.WeeklyTopSalesMedicineReport(request);
        }
    }
}
