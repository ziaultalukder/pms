using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Sales.Query
{
    public class GetSalesDetailsById: IRequest<GetSalesByIdViewModel>
    {
        public int Id { get; set; }
        public GetSalesDetailsById(int id)
        {
            Id = id;
        }
    }

    public class GetSalesDetailsByIdHandler : IRequestHandler<GetSalesDetailsById, GetSalesByIdViewModel>
    {
        private readonly ISalesService salesService;

        public GetSalesDetailsByIdHandler(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<GetSalesByIdViewModel> Handle(GetSalesDetailsById request, CancellationToken cancellationToken)
        {
            return await salesService.GetSalesDetailsById(request);
        }
    }
}
