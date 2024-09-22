using MediatR;
using Newtonsoft.Json.Linq;
using PMS.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PMS.Application.Request.Account.Command
{
    public class SendToken : IRequest<Result>
    {
        public string ContactNo { get; set; }
        public SendToken(string contactNo)
        {
            ContactNo = contactNo;
        }
    }

    public class SendTokenHandler : IRequestHandler<SendToken, Result>
    {
        private readonly IAccountsService userService;
        public SendTokenHandler(IAccountsService _userService)
        {
            userService = _userService;
        }

        public async Task<Result> Handle(SendToken request, CancellationToken cancellationToken)
        {
            var result = await userService.SendToken(request);
            return result;
        }

    }

}
