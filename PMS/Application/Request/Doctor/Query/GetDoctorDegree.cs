using MediatR;
using PMS.Models;

namespace PMS.Application.Request.Doctor.Query
{
    public class GetDoctorDegree : IRequest<IEnumerable<DoctorsDegree>>
    {

    }

    public class GetDoctorDegreeHandler : IRequestHandler<GetDoctorDegree, IEnumerable<DoctorsDegree>>
    {
        private readonly IDoctorService _doctorService;
        public GetDoctorDegreeHandler(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IEnumerable<DoctorsDegree>> Handle(GetDoctorDegree request, CancellationToken cancellationToken)
        {
            return await _doctorService.GetDoctorDegree();
        }
    }
}
