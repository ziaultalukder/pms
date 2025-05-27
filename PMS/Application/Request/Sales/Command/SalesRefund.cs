using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Sales.Command
{
    public class SalesRefund : IRequest<Result>
    {
        public int SalesDetailsId { get; set; }
        public int SalesInfoId { get; set; }
        public int MedicineId { get; set; }
        public int SalesQty { get; set; }
        public int RefundQty { get; set; }

        public SalesRefund(int salesDetailsId, int salesInfoId, int medicineId, int salesQty, int refundQty)
        {
            SalesDetailsId = salesDetailsId;
            SalesInfoId = salesInfoId;
            MedicineId = medicineId;
            SalesQty = salesQty;
            RefundQty = refundQty;
        }
    }

    public class SalesRefundHandler : IRequestHandler<SalesRefund, Result>
    {
        private readonly ISalesService salesService;

        public SalesRefundHandler(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<Result> Handle(SalesRefund request, CancellationToken cancellationToken)
        {
            return await salesService.SalesRefund(request);
        }
    }
}
