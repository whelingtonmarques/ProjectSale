using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSale.Entities
{
    /// <summary>
    /// Representa um produto.
    /// </summary>
    public class Product
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public int categoria { get; set; } = 0;
    }
}
