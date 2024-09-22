using PMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class SalesDetails:CommonEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SalesInfoId { get; set; }
        public int MedicineId { get; set; }
        public decimal StockPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int ExistingQuantiy { get; set; }
        public int Quantity { get; set; }
        public decimal TotalTaka { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountTaka { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }

    }
}
