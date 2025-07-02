using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Configuration.Command
{
    public class AddSupplier : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AddSupplier(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class AddSupplierHandler : IRequestHandler<AddSupplier, Result>
    {
        private readonly IConfigurationService _configurationService;
        public AddSupplierHandler(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public async Task<Result> Handle(AddSupplier request, CancellationToken cancellationToken)
        {
            return await _configurationService.AddSupplier(request);
        }
    }
}
