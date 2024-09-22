using MediatR;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account;
using PMS.Helpers;
using PMS.Domain.Models;

namespace PMS.Application.Request.Stock.Command
{
    public class AddNewStock : IRequest<Result>
    {
        public int Id { get; set; }
        public DateTime StockDate { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal DiscountValue { get; set; }
        public string IsActive { get; set; }
        public List<StockInDetails> StockInDetails { get; set; }
        public AddNewStock(int id, DateTime stockDate, int supplierId, decimal totalPrice, decimal discountPercentage, decimal discountTaka, decimal discountValue, string isActive, List<StockInDetails> stockInDetails)
        {
            Id = id; 
            StockDate = stockDate; 
            SupplierId = supplierId; 
            TotalPrice = totalPrice;
            DiscountPercentage = discountPercentage;
            DiscountTaka = discountTaka;
            DiscountValue = discountValue;
            IsActive = isActive;
            StockInDetails = stockInDetails;
        }
    }

    public class AddNewStockHandler : IRequestHandler<AddNewStock, Result>
    {
        private readonly IStockService stockService;
        public AddNewStockHandler(IStockService _stockService)
        {
            stockService = _stockService;
        }
        public async Task<Result> Handle(AddNewStock request, CancellationToken cancellationToken)
        {

            var result = await stockService.MedicineStock(request);
            return result;
        }
    }
}
