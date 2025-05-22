using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Sales.Query;
using PMS.Application.Request.Sales;
using PMS.ViewModel;

namespace PMS.Application.Request.Post.Query
{
    public class GetPost : PageParameters, IRequest<PagedList<GetPostViewModel>>
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }

        public GetPost(string title, int categoryId, int currentPage, int itemsPerpage) : base(currentPage, itemsPerpage)
        {
            Title = title;
            CategoryId = categoryId;
        }
    }

    public class GetPostHandler : IRequestHandler<GetPost, PagedList<GetPostViewModel>>
    {
        private readonly IPostService postService;

        public GetPostHandler(IPostService _postService)
        {
            postService = _postService;
        }
        public async Task<PagedList<GetPostViewModel>> Handle(GetPost request, CancellationToken cancellationToken)
        {
            return await postService.GetPost(request);
        }
    }
}
