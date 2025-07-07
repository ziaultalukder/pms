using MediatR;
using PMS.Application.Request.Sales.Command;
using PMS.Application.Request.Sales;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Stock.Query
{
    public class GetStockById : IRequest<StockInforIdViewModel>
    {
        public int Id { get; set; }
        public GetStockById(int id)
        {
            Id = id;
        }
    }

    public class GetStockByIdHandler : IRequestHandler<GetStockById, StockInforIdViewModel>
    {
        private readonly IStockService stockService;

        public GetStockByIdHandler(IStockService stockService)
        {
            this.stockService = stockService;
        }
        public async Task<StockInforIdViewModel> Handle(GetStockById request, CancellationToken cancellationToken)
        {
            return await stockService.GetStockById(request);
        }
    }
}
