using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Account.Query
{
    public class GetAdminProfile : IRequest<ProfileViewModel>
    {
        public GetAdminProfile() { }
    }

    public class GetAdminProfileHandler : IRequestHandler<GetAdminProfile, ProfileViewModel>
    {
        private readonly IAccountsService _accountsService;
        public GetAdminProfileHandler(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        public async Task<ProfileViewModel> Handle(GetAdminProfile request, CancellationToken cancellationToken)
        {
            return await _accountsService.AdminProfile(request);
        }
    }
}
