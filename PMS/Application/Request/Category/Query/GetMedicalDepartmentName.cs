using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Category.Query
{
    public class GetMedicalDepartmentName : IRequest<IEnumerable<MedicalDepartmentNameViewModel>>
    {

    }

    public class GetMedicalDepartmentNameHandler : IRequestHandler<GetMedicalDepartmentName, IEnumerable<MedicalDepartmentNameViewModel>>
    {
        private readonly ICategoryService categoryService;
        public GetMedicalDepartmentNameHandler(ICategoryService categoryService)
        {

            this.categoryService = categoryService;

        }
        public Task<IEnumerable<MedicalDepartmentNameViewModel>> Handle(GetMedicalDepartmentName request, CancellationToken cancellationToken)
        {
            return categoryService.GetMedicalDepartmentName();
        }
    }


}
