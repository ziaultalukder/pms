using PMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class ClientWiseMedicine:CommonEntity
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int ClientId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int Quantity { get; set; }

    }
}
