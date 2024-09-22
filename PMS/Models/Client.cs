using PMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class Client:CommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
    }
}
