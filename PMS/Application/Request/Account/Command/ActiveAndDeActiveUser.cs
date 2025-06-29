using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Account.Command
{
    public class ActiveAndDeActiveUser : IRequest<Result>
    {
        public int Id { get; set; }
        public string IsActive { get; set; }
        public ActiveAndDeActiveUser(int id, string isActive) { Id = id; IsActive = isActive; }
    }

    public class ActiveAndDeActiveUserHandler : IRequestHandler<ActiveAndDeActiveUser, Result>
    {
        private readonly IAccountsService userService;
        public ActiveAndDeActiveUserHandler(IAccountsService _userService)
        {
            userService = _userService;
        }
        public async Task<Result> Handle(ActiveAndDeActiveUser request, CancellationToken cancellationToken)
        {
            return await userService.ActiveAndDeActiveUser(request);
        }
    }
}
