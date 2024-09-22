namespace PMS.Helpers.Interface
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        int RoleId { get; }
        string UserName { get; }
        string EmployeeId { get; }
        public int ClientId { get; }
    }
}
