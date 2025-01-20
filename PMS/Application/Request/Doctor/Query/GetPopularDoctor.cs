using MediatR;
using PMS.ViewModel;

namespace PMS.Application.Request.Doctor.Query
{
    public class GetPopularDoctor : IRequest<IEnumerable<GetPopularDoctorViewModel>>
    {
    }

    public class GetPopularDoctorHandler : IRequestHandler<GetPopularDoctor, IEnumerable<GetPopularDoctorViewModel>>
    {
        private readonly IDoctorService _doctorService;
        public GetPopularDoctorHandler(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IEnumerable<GetPopularDoctorViewModel>> Handle(GetPopularDoctor request, CancellationToken cancellationToken)
        {
            return await _doctorService.GetPopularDoctor();
        }
    }
}
