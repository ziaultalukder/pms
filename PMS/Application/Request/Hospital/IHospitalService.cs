using PMS.Application.Request.Hospital.Query;
using PMS.ViewModel;

namespace PMS.Application.Request.Hospital
{
    public interface IHospitalService
    {
        Task<IEnumerable<DivisionAndDistrictWiseHospitalViewModel>> GetDivisionAndDistrictWiseHospital(GetDivisionAndDistrictWiseHospital request);
        Task<IEnumerable<Models.Hospital>> GetHospitalById(GetHospitalById request);
        Task<IEnumerable<PopularHospitalViewModel>> GetPopularHospitalInBangladesh();
    }
}
