using PMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class StockInDetails
    {
        public int Id { get; set; }
        //public int StockInfoId { get; set; }
        public int MedicineId { get; set; }
        //public int ExistingQty { get; set; }
        public int NewQty { get; set; }
        public decimal ExistingPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PruchasePrice { get; set; }

    }
}
