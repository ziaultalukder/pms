namespace PMS.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
    }
}
