using MediatR;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account;
using PMS.Helpers;

namespace PMS.Application.Request.Configuration.Command
{
    public class UpdateClient : IRequest<Result>
    {
        public int Id { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string BillingCycle { get; set; }
        public int SubscriptionValue { get; set; }

        public UpdateClient(int id, int subscriptionPlanId, string name, string shopName, string address, string contactNo, string email, string billingCycle, int subscriptionValue)
        {
            Id = id;
            SubscriptionPlanId = subscriptionPlanId;
            Name = name;
            ShopName = shopName;
            Address = address;
            ContactNo = contactNo;
            Email = email;
            BillingCycle = billingCycle;
            SubscriptionValue = subscriptionValue;
        }
    }

    public class UpdateClientHandler : IRequestHandler<UpdateClient, Result>
    {
        private readonly IConfigurationService configurationService;
        public UpdateClientHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<Result> Handle(UpdateClient request, CancellationToken cancellationToken)
        {
            var result = await configurationService.UpdateClient(request);
            return result;
        }
    }
}
