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
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Categoria { get; set; } = 0;
    }
}
