namespace PMS.Models
{
    public class Doctor
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

    }
}
