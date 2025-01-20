using PMS.Application.Request.Doctor.Command;
using PMS.Application.Request.Doctor.Query;
using PMS.Helpers;
using PMS.Models;
using PMS.ViewModel;

namespace PMS.Application.Request.Doctor
{
    public interface IDoctorService
    {
        Task<Result> AddDoctor(AddDoctor request);
        Task<IEnumerable<GetDoctorByCategoryIdViewModel>> GetDoctorCategoryId(GetDoctorCategoryId request);
        Task<IEnumerable<DoctorsDegree>> GetDoctorDegree();
        Task<IEnumerable<GetPopularDoctorViewModel>> GetPopularDoctor();
        Task<Result> UpdateDoctor(UpdateDoctor request);
    }
}
