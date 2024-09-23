using MediatR;
using PMS.Application.Common.Pagins;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration.Query
{
    public class GetClientWiseMedicine : PageParameters, IRequest<PagedList<ClientWiseMedicineViewModel>>
    {
        public string MedicineName { get; set; }
        public string GetAll { get; set; }
        public GetClientWiseMedicine(string medicineName, string getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            MedicineName = medicineName;
            GetAll = getAll;
        }
    }

    public class ClientWiseMedicineViewModelHandler : IRequestHandler<GetClientWiseMedicine, PagedList<ClientWiseMedicineViewModel>>
    {
        private readonly IConfigurationService configurationService;

        public ClientWiseMedicineViewModelHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<PagedList<ClientWiseMedicineViewModel>> Handle(GetClientWiseMedicine request, CancellationToken cancellationToken)
        {
            return await configurationService.ClientWiseMedicine(request);
        }
    }
}
