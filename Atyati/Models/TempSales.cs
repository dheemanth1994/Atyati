using System;
using System.Collections.Generic;

namespace Atyati.Models
{
    public partial class TempSales
    {
        public int Pid { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Brand { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsOutOfStock { get; set; }
        public Category Category { get; set; }
    }
}
