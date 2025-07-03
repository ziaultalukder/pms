using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration.Query
{
    public class SupplierByName : IRequest<IEnumerable<SupplierByNameViewModel>>
    {
        public string Name { get; set; }
        public SupplierByName(string name)
        {
            Name = name;
        }
    }

    public class SupplierByNameHandler : IRequestHandler<SupplierByName, IEnumerable<SupplierByNameViewModel>>
    {
        private readonly IConfigurationService configurationService;
        public SupplierByNameHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<IEnumerable<SupplierByNameViewModel>> Handle(SupplierByName request, CancellationToken cancellationToken)
        {
            return await configurationService.SupplierByName(request);
        }
    }
}
