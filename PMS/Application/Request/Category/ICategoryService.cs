using PMS.ViewModel;

namespace PMS.Application.Request.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<MedicalDepartmentNameViewModel>> GetMedicalDepartmentName();
    }
}
