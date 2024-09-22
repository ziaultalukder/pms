using PMS.Helpers.Interface;
using System.Security.Claims;

namespace PMS.Helpers
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
            ClientId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email));
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }
        public int UserId { get; }
        public int RoleId { get; }
        public string UserName { get; }
        public string EmployeeId { get; }
        public int ClientId { get ; }
    }
}
