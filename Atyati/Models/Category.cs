using System;
using System.Collections.Generic;

namespace Atyati.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
            Sold = new HashSet<Sold>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Product> Product { get; set; }
        public ICollection<Sold> Sold { get; set; }
    }
}
