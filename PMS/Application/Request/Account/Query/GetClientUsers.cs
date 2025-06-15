using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Account.Query
{
    public class GetClientUsers : IRequest<IEnumerable<ClientUserViewModel>>
    {
        public GetClientUsers() { }
    }

    public class GetClientUsersHandler : IRequestHandler<GetClientUsers, IEnumerable<ClientUserViewModel>>
    {
        private readonly IAccountsService _accountsService;
        public GetClientUsersHandler(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        public async Task<IEnumerable<ClientUserViewModel>> Handle(GetClientUsers request, CancellationToken cancellationToken)
        {
            return await _accountsService.GetClientUsers(request);
        }
    }
}
