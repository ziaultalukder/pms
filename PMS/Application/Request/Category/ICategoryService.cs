using PMS.Application.Request.Category.Query;
using PMS.ViewModel;

namespace PMS.Application.Request.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<Models.Category>> GetCategoryByTypeId(GetCategoryById request);
        Task<IEnumerable<MedicalDepartmentNameViewModel>> GetMedicalDepartmentName();
    }
}
