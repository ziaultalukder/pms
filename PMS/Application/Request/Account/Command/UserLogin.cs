using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Account.Command
{
    public class UserLogin : IRequest<object>
    {
        public string ContactNo { get; set; }
        public string Password { get; set; }
        public UserLogin(string contactNo, string password)
        {
            ContactNo = contactNo;
            Password = password;
        }
    }

    public class UserLoginHandler : IRequestHandler<UserLogin, object>
    {
        private readonly IAccountsService accountsService;
        public UserLoginHandler(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        public async Task<object> Handle(UserLogin request, CancellationToken cancellationToken)
        {
            var result = await accountsService.UserLogin(request);
            return result;
        }
    }


}
