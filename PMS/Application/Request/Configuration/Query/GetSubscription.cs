using MediatR;
using PMS.Application.Request.Configuration.Command;
using PMS.Helpers;
using PMS.Models;

namespace PMS.Application.Request.Configuration.Query
{
    public class GetSubscription : IRequest<IEnumerable<Subscription>>
    {
    }

    public class GetSubscriptionHandler : IRequestHandler<GetSubscription, IEnumerable<Subscription>>
    {
        private readonly IConfigurationService _configurationService;
        public GetSubscriptionHandler(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public async Task<IEnumerable<Subscription>> Handle(GetSubscription request, CancellationToken cancellationToken)
        {
            return await _configurationService.GetSubscription();
        }
    }
}
