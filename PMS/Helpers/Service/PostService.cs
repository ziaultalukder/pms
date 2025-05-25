using Dapper;

using PMS.Application.Common.Pagins;
using PMS.Application.Request.Post;
using PMS.Application.Request.Post.Command;
using PMS.Application.Request.Post.Query;
using PMS.Context;
using PMS.Helpers.Interface;
using PMS.ViewModel;
using System.Data;

namespace PMS.Helpers.Service
{
    public class PostService : IPostService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostEnvironment;
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;
        public PostService(DapperContext dapperContext, ICurrentUserService currentUserService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnvironment)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Result> CreatePost(CreatePost request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                var featuredImage = Helper.SaveSingleImage(request.FeaturedImage, Constant.POST_IMAGE, _hostEnvironment);
                if (!featuredImage.Result.Succeed)
                {
                    return Result.Failure(new List<string> { "Post Image Not Saved!!!!" });
                }
                else
                {
                    request.FeaturedImage = featuredImage.Result.Message;
                }

                var postImage = Helper.SaveSingleImage(request.Image, Constant.POST_IMAGE, _hostEnvironment);
                if (!postImage.Result.Succeed)
                {
                    return Result.Failure(new List<string> { "Post Image Not Saved!!!!" });
                }
                else
                {
                    request.Image = postImage.Result.Message;
                }

                string query = "InsertBlogPorst";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@CategoryId", request.CategoryId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Title", request.Title, DbType.String, ParameterDirection.Input);
                parameter.Add("@SeoTitle", request.Title.Replace(" ", "-"), DbType.String, ParameterDirection.Input);
                parameter.Add("@Description", request.Description, DbType.String, ParameterDirection.Input);
                parameter.Add("@FeaturedImage", request.FeaturedImage, DbType.String, ParameterDirection.Input);
                parameter.Add("@Image", request.Image, DbType.String, ParameterDirection.Input);

                parameter.Add("@Tags", request.Tags, DbType.String, ParameterDirection.Input);

                parameter.Add("@CreateBy", _currentUserService.UserId, DbType.String, ParameterDirection.Input);
                parameter.Add("@MESSAGE", "", DbType.Int32, ParameterDirection.Output);

                var result = await context.ExecuteAsync(query, parameter);
                int res = parameter.Get<int>("@MESSAGE");

                return Result.Success("Save Success");
            }
        }
        public async Task<Result> UpdatePost(UpdatePost request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                /*featured image start*/
                if (!ImageDirectory.IsFileExists(_hostEnvironment, request.FeaturedImage))
                {
                    var featuredImage = Helper.SaveSingleImage(request.FeaturedImage, Constant.POST_IMAGE, _hostEnvironment);
                    if (!featuredImage.Result.Succeed)
                    {
                        return Result.Failure(new List<string> { "Post Image Not Saved!!!!" });
                    }
                    else
                    {
                        request.FeaturedImage = featuredImage.Result.Message;
                    }
                }
                /*featured image end*/

                /*post image start*/
                if (!ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    var postImage = Helper.SaveSingleImage(request.Image, Constant.POST_IMAGE, _hostEnvironment);
                    if (!postImage.Result.Succeed)
                    {
                        return Result.Failure(new List<string> { "Post Image Not Saved!!!!" });
                    }
                    else
                    {
                        request.Image = postImage.Result.Message;
                    }
                }
                
                /*post image end*/

                string query = "UpdateBlogPorst";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@CategoryId", request.CategoryId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Title", request.Title, DbType.String, ParameterDirection.Input);
                parameter.Add("@SeoTitle", request.Title.Replace(" ", "-"), DbType.String, ParameterDirection.Input);
                parameter.Add("@Description", request.Description, DbType.String, ParameterDirection.Input);
                parameter.Add("@FeaturedImage", request.FeaturedImage, DbType.String, ParameterDirection.Input);
                parameter.Add("@Image", request.Image, DbType.String, ParameterDirection.Input);

                parameter.Add("@Tags", request.Tags, DbType.String, ParameterDirection.Input);

                parameter.Add("@CreateBy", _currentUserService.UserId, DbType.String, ParameterDirection.Input);
                parameter.Add("@MESSAGE", "", DbType.Int32, ParameterDirection.Output);

                var result = await context.ExecuteAsync(query, parameter);
                int res = parameter.Get<int>("@MESSAGE");

                return Result.Success("Update Success");
            }
        }
        public async Task<PagedList<GetPostViewModel>> GetPost(GetPost request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string conditionClause = " ";
                string query = "select GetBlogPost.*, count(*) over() as TotalItems from GetBlogPost ";

                if (!string.IsNullOrEmpty(request.Title))
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " Title like '" + request.Title.Trim() + "%' ";
                    conditionClause = " WHERE ";
                }
                if (request.CategoryId > 0)
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " CategoryId = " + request.CategoryId + " ";
                    conditionClause = " WHERE ";
                }

                query += " order by Id OFFSET " + ((request.CurrentPage - 1) * request.ItemsPerPage) + " ROWS FETCH NEXT " + request.ItemsPerPage + " ROWS ONLY ";

                var postList = await context.QueryAsync<GetPostViewModel>(query);

                return new PagedList<GetPostViewModel>(postList.ToList(), request.CurrentPage, request.ItemsPerPage, postList.Count());
            }
        }
        public async Task<GetPostByIdViewModel> GetPostById(GetPostById request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                var parameters = new { Id = request.Id };
                var sql = "SELECT * from GetBlogPost where Id = @Id";
                return await context.QueryFirstOrDefaultAsync<GetPostByIdViewModel>(sql, parameters);
            }
        }
    }
}
