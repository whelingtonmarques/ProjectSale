using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSale.Entities
{
    /// <summary>
    /// Representa um pedido.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// O identificador único do pedido.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// O identificador exclusivo do pedido.
        /// </summary>
        public string identificador { get; set; } = string.Empty;

        /// <summary>
        /// A descrição do pedido.
        /// </summary>
        public string descricao { get; set; } = string.Empty;

        /// <summary>
        /// O valor total do pedido.
        /// </summary>
        public decimal valorTotal { get; set; } = 0;
    }
}
