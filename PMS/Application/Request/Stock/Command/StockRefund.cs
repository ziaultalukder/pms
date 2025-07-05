using MediatR;
using PMS.Application.Request.Sales.Command;
using PMS.Application.Request.Sales;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Stock.Command
{
    public class StockRefund : IRequest<Result>
    {
        public int Id { get; set; }
        public decimal TotalTaka { get; set; }
        public int Discount { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal GrandTotal { get; set; }

        public IEnumerable<StockDetailsViewModelForRefund> RefundDetails { get; set; }

        public StockRefund(int id, decimal totalTaka, int discount, decimal discountTaka, decimal grandTotal, IEnumerable<StockDetailsViewModelForRefund> refundDetails)
        {
            Id = id;
            TotalTaka = totalTaka;
            Discount = discount;
            DiscountTaka = discountTaka;
            GrandTotal = grandTotal;
            RefundDetails = refundDetails;
        }
    }

    public class StockRefundHandler : IRequestHandler<StockRefund, Result>
    {
        private readonly IStockService _stockService;

        public StockRefundHandler(IStockService stockService)
        {
            _stockService = stockService;
        }
        public async Task<Result> Handle(StockRefund request, CancellationToken cancellationToken)
        {
            return await _stockService.StockRefund(request);
        }
    }
}
