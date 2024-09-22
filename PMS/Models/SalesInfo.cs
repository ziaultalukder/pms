using PMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class SalesInfo:CommonEntity
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public decimal TotalTaka { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal GrandTotal { get; set; }
        public int ClientId { get; set; }
        public string InvoiceNo { get; set; }
    }
}
