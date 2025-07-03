using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Configuration.Command
{
    public class UpdateMedicine : IRequest<Result>
    {
        public int Id { get; set; }
        public int ManufactureId { get; set; }
        public string Manufacturer { get; set; }
        public string BrandName { get; set; }

        public UpdateMedicine(int id,  string manufacturer, string brandName, int manufactureId)
        {
            Id = id;
            Manufacturer = manufacturer;
            BrandName = brandName;
            ManufactureId = manufactureId;
        }
    }

    public class UpdateMedicineHandler : IRequestHandler<UpdateMedicine, Result>
    {
        private readonly IConfigurationService _configurationService;
        public UpdateMedicineHandler(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public async Task<Result> Handle(UpdateMedicine request, CancellationToken cancellationToken)
        {
            return await _configurationService.UpdateMedicine(request);
        }
    }
}
