using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Models;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration.Query
{
    public class MedicineListByName : IRequest<IEnumerable<MedicineListByNameViewModel>>
    {
        public string Name { get; set; }
        public MedicineListByName(string name)
        {
            Name = name;
        }
    }

    public class MedicineListByNameHandler : IRequestHandler<MedicineListByName, IEnumerable<MedicineListByNameViewModel>>
    {
        private readonly IConfigurationService configurationService;
        public MedicineListByNameHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<IEnumerable<MedicineListByNameViewModel>> Handle(MedicineListByName request, CancellationToken cancellationToken)
        {
            return await configurationService.MedicineListByName(request);
        }
    }
}
