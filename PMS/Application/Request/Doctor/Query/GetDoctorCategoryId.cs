using MediatR;
using PMS.ViewModel;
using System.Formats.Asn1;

namespace PMS.Application.Request.Doctor.Query
{
    public class GetDoctorCategoryId : IRequest<IEnumerable<GetDoctorByCategoryIdViewModel>>
    {
        public int CategoryId { get; set; }
        public GetDoctorCategoryId(int categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class GetDoctorCategoryIdHandler : IRequestHandler<GetDoctorCategoryId, IEnumerable<GetDoctorByCategoryIdViewModel>>
    {
        private readonly IDoctorService _doctorService;
        public GetDoctorCategoryIdHandler(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IEnumerable<GetDoctorByCategoryIdViewModel>> Handle(GetDoctorCategoryId request, CancellationToken cancellationToken)
        {
            return await _doctorService.GetDoctorCategoryId(request);
        }
    }
}
