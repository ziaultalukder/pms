using MediatR;

namespace PMS.Application.Request.Category.Query
{
    public class GetCategoryById : IRequest<IEnumerable<Models.Category>>
    {
        public int CategoryTypeId { get; set; }
        public GetCategoryById(int categoryTypeId)
        {
            CategoryTypeId = categoryTypeId;
        }
    }

    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, IEnumerable<Models.Category>>
    {
        private readonly ICategoryService categoryService;
        public GetCategoryByIdHandler(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IEnumerable<Models.Category>> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            return await categoryService.GetCategoryByTypeId(request);
        }
    }
}
