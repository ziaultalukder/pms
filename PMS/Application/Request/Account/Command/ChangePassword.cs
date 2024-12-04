using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Account.Command
{
    public class ChangePassword : IRequest<Result>
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePassword(int id, string oldPassword, string newPassword)
        {
            Id = id;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePassword, Result>
    {
        private readonly IAccountsService accountsService;
        public ChangePasswordHandler(IAccountsService _accountsService)
        {
            accountsService = _accountsService;
        }
        public async Task<Result> Handle(ChangePassword request, CancellationToken cancellationToken)
        {
            return await accountsService.ChangePassword(request);
        }
    }
}
