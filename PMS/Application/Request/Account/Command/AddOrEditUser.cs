using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Account.Command
{
    public class AddOrEditUser : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int ClientId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string IsActive { get; set; }
        public string Status { get; set; }

        public AddOrEditUser(int id, string name, string email, string mobile, int clientId, string password, int roleId, string isActive, string status)
        {
            Id = id;
            Name = name;
            Email = email;
            Mobile = mobile;
            ClientId = clientId;
            Password = password;
            RoleId = roleId;
            IsActive = isActive;
            Status = status;
        }
    }

    public class AddOrEditUserHandler : IRequestHandler<AddOrEditUser, Result>
    {
        private readonly IAccountsService userService;
        public AddOrEditUserHandler(IAccountsService _userService)
        {
            userService = _userService;
        }
        public async Task<Result> Handle(AddOrEditUser request, CancellationToken cancellationToken)
        {

            var result = await userService.AddOrEditUser(request);
            return result;
        }
    }

}
