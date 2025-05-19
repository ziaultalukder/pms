using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Sales.Query;
using PMS.Application.Request.Sales;
using PMS.ViewModel;

namespace PMS.Application.Request.Stock.Query
{
    public class GetStock : PageParameters, IRequest<PagedList<GetStockViewModel>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public GetStock(string startDate, string endDate, int currentPage, int itemsPerpage) : base(currentPage, itemsPerpage)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class GetStockHandler : IRequestHandler<GetStock, PagedList<GetStockViewModel>>
    {
        private readonly IStockService stockService;

        public GetStockHandler(IStockService _stockService)
        {
            stockService = _stockService;
        }
        public async Task<PagedList<GetStockViewModel>> Handle(GetStock request, CancellationToken cancellationToken)
        {
            return await stockService.GetStocks(request);
        }
    }
}
