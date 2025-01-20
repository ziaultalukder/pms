using MediatR;

namespace PMS.Application.Request.Hospital.Query
{
    public class GetHospitalById : IRequest<IEnumerable<Models.Hospital>>
    {
        public int Id { get; set; }
        public GetHospitalById(int id) { Id = id; }
    }

    public class GetHospitalByIdHandler : IRequestHandler<GetHospitalById, IEnumerable<Models.Hospital>>
    {
        private readonly IHospitalService _service;
        public GetHospitalByIdHandler(IHospitalService service)
        {

            _service = service;

        }
        public async Task<IEnumerable<Models.Hospital>> Handle(GetHospitalById request, CancellationToken cancellationToken)
        {
            return await _service.GetHospitalById(request);
        }
    }
}
