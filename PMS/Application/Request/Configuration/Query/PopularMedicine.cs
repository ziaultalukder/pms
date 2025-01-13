using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration.Query
{
    public class PopularMedicine : IRequest<IEnumerable<PopularMedicineViewModel>>
    {
        public PopularMedicine()
        {

        }
    }

    public class PopularMedicineHandler : IRequestHandler<PopularMedicine, IEnumerable<PopularMedicineViewModel>>
    {
        private readonly IConfigurationService configurationService;
        public PopularMedicineHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }

        public async Task<IEnumerable<PopularMedicineViewModel>> 
            Handle(PopularMedicine request, CancellationToken cancellationToken)
        {
            return await configurationService.PopularMedicine();
        }
    }
}
