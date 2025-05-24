using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Post.Query
{
    public class GetPostById : IRequest<GetPostByIdViewModel>
    {
        public int Id { get; set; }
        public GetPostById(int id) { Id = id; }
    }
    
    public class GetPostByIdHandler : IRequestHandler<GetPostById, GetPostByIdViewModel>
    {
        private readonly IPostService _postService;

        public GetPostByIdHandler(IPostService postService) 
        { 
            _postService = postService;
        }

        public Task<GetPostByIdViewModel> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            return _postService.GetPostById(request);
        }
    }
}
