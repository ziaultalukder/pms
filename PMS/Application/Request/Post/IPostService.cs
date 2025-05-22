using PMS.Application.Common.Pagins;
using PMS.Application.Request.Post.Command;
using PMS.Application.Request.Post.Query;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Post
{
    public interface IPostService
    {
        Task<Result> CreatePost(CreatePost request);
        Task<PagedList<GetPostViewModel>> GetPost(GetPost request);
        Task<Result> UpdatePost(UpdatePost request);
    }
}
