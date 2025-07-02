using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Models;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration.Query
{
    public class GetUserUploadItem : PageParameters, IRequest<PagedList<GetUserUploadItemViewModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GetAll { get; set; }
        public GetUserUploadItem(int id, string name, string getAll, int currentPage, int itemsPerpage) : base(currentPage, itemsPerpage)
        {
            Id = id;
            Name = name;
            GetAll = getAll;
        }
    }

    public class GetUserUploadItemHandler : IRequestHandler<GetUserUploadItem, PagedList<GetUserUploadItemViewModel>>
    {
        private readonly IConfigurationService configurationService;
        public GetUserUploadItemHandler(IConfigurationService _configurationService)
        {
            configurationService = _configurationService;
        }
        public async Task<PagedList<GetUserUploadItemViewModel>> Handle(GetUserUploadItem request, CancellationToken cancellationToken)
        {
            return await configurationService.GetUserUploadItem(request);
        }
    }
}
