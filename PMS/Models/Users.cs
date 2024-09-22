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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int ClientId { get; set; }
        public string Password { get; set; }
        public string PasswordValue { get; set; }
        public string PasswordKey { get; set; }
        public int RoleId { get; set; }
        public string IsActive { get; set; }
        public string Status { get; set; }
    }
}
