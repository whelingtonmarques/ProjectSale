﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSale.Entities {
    /// <summary>
    /// Representa um item de pedido.
    /// </summary>
    public class ItemsOrder {

        /// <summary>
        /// O identificador único do item de pedido.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// O ID do pedido ao qual o item pertence.
        /// </summary>
        public int pedidoId { get; set; }

        /// <summary>
        /// O ID do produto associado ao item.
        /// </summary>
        public int produtoId { get; set; }

        /// <summary>
        /// O valor unitário do produto no item de pedido.
        /// </summary>
        public float valor { get; set; }

        /// <summary>
        /// A quantidade do produto no item de pedido.
        /// </summary>
        public int quantidade { get; set; }
    }
}
