using MediatR;
using PMS.Application.Common.Pagins;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration.Query
{
    public class MedicineList : PageParameters, IRequest<PagedList<MedicineListViewModel>>
    {
        public string Name { get; set; }
        public string GetAll { get; set; }
        public MedicineList(string name, string getAll, int currentPage, int itemsPerPage) :base(currentPage, itemsPerPage)
        {
            Name = name;
            GetAll = getAll;
        }
    }
    
    public class MedicineList1 : PageParameters, IRequest<PagedList<MedicineListViewModel>>
    {
        public string Name { get; set; }
        public string GetAll { get; set; }
        public MedicineList1(string name, string getAll, int currentPage, int itemsPerPage) :base(currentPage, itemsPerPage)
        {
            Name = name;
            GetAll = getAll;
        }
    }

    public class MedicineListHandler : IRequestHandler<MedicineList, PagedList<MedicineListViewModel>>
    {
        private readonly IConfigurationService configurationService;
        public MedicineListHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<PagedList<MedicineListViewModel>> Handle(MedicineList request, CancellationToken cancellationToken)
        {
            return await configurationService.MedicineList(request);
        }
    }


}
