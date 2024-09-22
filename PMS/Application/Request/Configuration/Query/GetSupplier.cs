using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Domain.Models;
using PMS.Models;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration.Query
{
    public class GetSupplier : PageParameters, IRequest<PagedList<Supplier>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GetAll { get; set; }
        public GetSupplier(int id, string name, string getAll,int currentPage, int itemsPerpage) : base(currentPage, itemsPerpage)
        {
            Id = id;
            Name = name;
            GetAll = getAll;
        }
    }

    public class GetSupplierHandler : IRequestHandler<GetSupplier, PagedList<Supplier>>
    {
        private readonly IConfigurationService configurationService;
        public GetSupplierHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<PagedList<Supplier>> Handle(GetSupplier request, CancellationToken cancellationToken)
        {
            return await configurationService.GetSupplier(request);
        }
    }

}
