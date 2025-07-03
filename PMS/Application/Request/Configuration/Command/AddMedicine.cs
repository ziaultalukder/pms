using MediatR;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account;
using PMS.Helpers;

namespace PMS.Application.Request.Configuration.Command
{
    public class AddMedicine : IRequest<Result>
    {
        public int ManufactureId { get; set; }
        public string Manufacturer { get; set; }
        public string BrandName { get; set; }

        public AddMedicine(string manufacturer, string brandName, int manufactureId)
        {
            Manufacturer = manufacturer;
            BrandName = brandName;
            ManufactureId = manufactureId;

        }
    }

    public class AddMedicineHandler : IRequestHandler<AddMedicine, Result>
    {
        private readonly IConfigurationService _configurationService;
        public AddMedicineHandler(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public async Task<Result> Handle(AddMedicine request, CancellationToken cancellationToken)
        {
            return await _configurationService.AddMedicine(request);
        }
    }
}
