using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Hospital.Query
{
    public class GetDivisionAndDistrictWiseHospital : IRequest<IEnumerable<DivisionAndDistrictWiseHospitalViewModel>>
    {
        public int DivisionId { get; set; }
        public GetDivisionAndDistrictWiseHospital(int divisionId)
        {
            DivisionId = divisionId;
        }
    }

    public class GetDivisionAndDistrictWiseHospitalHandler : IRequestHandler<GetDivisionAndDistrictWiseHospital, IEnumerable<DivisionAndDistrictWiseHospitalViewModel>>
    {
        private readonly IHospitalService _service;
        public GetDivisionAndDistrictWiseHospitalHandler(IHospitalService service)
        {

            _service = service;

        }
        public async Task<IEnumerable<DivisionAndDistrictWiseHospitalViewModel>> Handle(GetDivisionAndDistrictWiseHospital request, CancellationToken cancellationToken)
        {
            return await _service.GetDivisionAndDistrictWiseHospital(request);
        }
    }
}
