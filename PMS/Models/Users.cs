using PMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class Users:CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public string Password { get; set; }=string.Empty;
        public int RoleId { get; set; }
        public string IsActive { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
