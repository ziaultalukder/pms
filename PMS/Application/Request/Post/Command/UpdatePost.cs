using MediatR;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Post.Query;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Post.Command
{
    public class UpdatePost : IRequest<Result>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FeaturedImage { get; set; }
        public string Image { get; set; }
        public string Tags { get; set; }

        public UpdatePost(int categoryId, string title, string description, string featuredImage, string image, string tags) 
        { 
            CategoryId = categoryId;
            Title = title;
            Description = description;
            FeaturedImage = featuredImage;
            Image = image;
            Tags = tags;
        }
    }

    public class UpdatePostHandler : IRequestHandler<UpdatePost, Result>
    {
        private readonly IPostService postService;

        public UpdatePostHandler(IPostService _postService)
        {
            postService = _postService;
        }
        public async Task<Result> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            return await postService.UpdatePost(request);
        }
    }
}
