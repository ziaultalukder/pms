using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Configuration.Command
{
    public class AddOrEditSubscription : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string BillingCycle { get; set; } // Monthly, Yearly, etc.
        public bool IsActive { get; set; }
        public AddOrEditSubscription(int id, string name, string description, decimal price, string billingCycle, bool isActive)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            BillingCycle = billingCycle;
            IsActive = isActive;
        }
    }

    public class AddOrEditSubscriptionHandler : IRequestHandler<AddOrEditSubscription, Result>
    {
        private readonly IConfigurationService _configurationService;
        public AddOrEditSubscriptionHandler(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public async Task<Result> Handle(AddOrEditSubscription request, CancellationToken cancellationToken)
        {
            return await _configurationService.AddOrEditSubscription(request);
        }
    }
}
