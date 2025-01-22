using MediatR;
using PMS.Models;

namespace PMS.Application.Request.Configuration.Query
{
    public class GetDivision : IRequest<IEnumerable<Division>>
    {
    }

    public class GetDivisionHandler : IRequestHandler<GetDivision, IEnumerable<Division>>
    {
        private readonly IConfigurationService configurationService;
        public GetDivisionHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<IEnumerable<Division>> Handle(GetDivision request, CancellationToken cancellationToken)
        {
            return await configurationService.GetDivision();
        }
    }
}
