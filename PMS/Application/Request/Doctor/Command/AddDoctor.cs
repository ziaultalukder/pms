using MediatR;
using PMS.Helpers;

namespace PMS.Application.Request.Doctor.Command
{
    public class AddDoctor: IRequest<Result>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string VisitingHoure { get; set; }
        public string Chember { get; set; }

        public AddDoctor(int categoryId, string name, string image, string contactNo, string email, string tags, string description, string visitingHoure, string chember)
        {
            CategoryId = categoryId;
            Name = name;
            Image = image;
            ContactNo = contactNo;
            Email = email;
            Tags = tags;
            Description = description;
            VisitingHoure = visitingHoure;
            Chember = chember;
        }
    }

    public class AddDoctorHandler : IRequestHandler<AddDoctor, Result>
    {
        private readonly IDoctorService doctorService;
        public AddDoctorHandler(IDoctorService doctorService)
        {

            this.doctorService = doctorService;

        }
        public Task<Result> Handle(AddDoctor request, CancellationToken cancellationToken)
        {
            return doctorService.AddDoctor(request);
        }
    }
}
