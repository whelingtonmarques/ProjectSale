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
        public int Id { get; set; }

        /// <summary>
        /// O identificador exclusivo do pedido.
        /// </summary>
        public string Identificador { get; set; } = string.Empty;

        /// <summary>
        /// A descrição do pedido.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// O valor total do pedido.
        /// </summary>
        public decimal ValorTotal { get; set; } = 0;
    }
}
