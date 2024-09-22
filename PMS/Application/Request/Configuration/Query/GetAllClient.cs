using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Account;
using PMS.Domain.Models;

namespace PMS.Application.Request.Configuration.Query
{
    public class GetAllClient : PageParameters, IRequest<PagedList<Client>>
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string ContactNo { get; set; }
        public string GetAll { get; set; }
        public GetAllClient(int id, string shopName, string contactNo, string getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            ShopName = shopName;
            ContactNo = contactNo;
            GetAll = getAll;
        }
    }

    public class GetAllClientHandler : IRequestHandler<GetAllClient, PagedList<Client>>
    {
        private readonly IConfigurationService configurationService;
        public GetAllClientHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<PagedList<Client>> Handle(GetAllClient request, CancellationToken cancellationToken)
        {
            return await configurationService.GetAllClient(request);
        }
    }

}
