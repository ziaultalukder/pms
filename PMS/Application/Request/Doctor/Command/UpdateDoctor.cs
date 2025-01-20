using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Doctor.Command
{
    public class UpdateDoctor : IRequest<Result>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string VisitingHoure { get; set; }
        public string Chember { get; set; }

        public UpdateDoctor(int id, int categoryId, string name, string image, string contactNo, string email, string description, string visitingHoure, string chember)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Image = image;
            ContactNo = contactNo;
            Email = email;
            Description = description;
            VisitingHoure = visitingHoure;
            Chember = chember;
        }
    }

    public class UpdateDoctorHandler : IRequestHandler<UpdateDoctor, Result>
    {
        private readonly IDoctorService doctorService;
        public UpdateDoctorHandler(IDoctorService doctorService)
        {

            this.doctorService = doctorService;

        }
        public Task<Result> Handle(UpdateDoctor request, CancellationToken cancellationToken)
        {
            return doctorService.UpdateDoctor(request);
        }
    }
}
