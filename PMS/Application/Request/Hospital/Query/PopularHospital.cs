using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Hospital.Query
{
    public class PopularHospital : IRequest<IEnumerable<PopularHospitalViewModel>>
    {

    }

    public class PopularHospitalHandler : IRequestHandler<PopularHospital, IEnumerable<PopularHospitalViewModel>>
    {
        private readonly IHospitalService _service;
        public PopularHospitalHandler(IHospitalService service)
        {

            _service = service;

        }
        public async Task<IEnumerable<PopularHospitalViewModel>> Handle(PopularHospital request, CancellationToken cancellationToken)
        {
            return await _service.GetPopularHospitalInBangladesh();
        }
    }
}
