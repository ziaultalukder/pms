using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Account.Command
{
    public class AddOrEditClient : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string Password { get; set; }

        public AddOrEditClient(int id, string name, string shopName, string address, string contactNo, string email, string isActive, string password)
        {
            Id = id;
            Name = name;
            ShopName = shopName;
            Address = address;
            ContactNo = contactNo;
            Email = email;
            IsActive = isActive;
            Password = password;
        }
    }


    public class AddOrEditClientHandler : IRequestHandler<AddOrEditClient, Result>
    {
        private readonly IAccountsService userService;
        public AddOrEditClientHandler(IAccountsService _userService)
        {
            userService = _userService;
        }
        public async Task<Result> Handle(AddOrEditClient request, CancellationToken cancellationToken)
        {
            var result = await userService.AddOrEditClient(request);
            return result;
        }
    }
}
