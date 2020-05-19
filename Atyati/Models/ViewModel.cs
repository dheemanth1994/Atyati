using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Atyati.Models
{
    public class ViewModel
    {
        [Key]
        public int CartId { get; set; }
        
        public IEnumerable<Product> ProductsToBeSold { get; set; }
        public IEnumerable<TempSales> ProductsInCart { get; set; }

    }
}
