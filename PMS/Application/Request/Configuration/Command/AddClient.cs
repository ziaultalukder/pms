using MediatR;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account;
using PMS.Helpers;

namespace PMS.Application.Request.Configuration.Command
{
    public class AddClient : IRequest<Result>
    {
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AddClient(string name, string shopName, string address, string contactNo, string email, string password)
        {
            Name = name;
            ShopName = shopName;
            Address = address;
            ContactNo = contactNo;
            Email = email;
            Password = password;
        }
    }

    public class AddClientHandler : IRequestHandler<AddClient, Result>
    {
        private readonly IConfigurationService configurationService;
        public AddClientHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<Result> Handle(AddClient request, CancellationToken cancellationToken)
        {
            var result = await configurationService.AddClient(request);
            return result;
        }
    }
}
