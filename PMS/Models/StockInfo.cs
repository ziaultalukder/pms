using PMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class StockInfo:CommonEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime StockDate { get; set; }
        public string Invoice { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal DiscountValue { get; set; }
        public string IsActive { get; set; }
    }
}
