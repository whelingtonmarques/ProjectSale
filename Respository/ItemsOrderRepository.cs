using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProjectSale.Data;
using ProjectSale.Entities;

namespace ProjectSale.Respository {
    /// <summary>
    /// Classe de repositório para gerenciar entidades ItemsOrder no banco de dados.
    /// </summary>
    public class ItemsOrderRepository {
        private AppDbContext context = new();

        /// <summary>
        /// Retorna um ItemsOrder pelo seu ID de produto.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>O ItemsOrder associado ao ID do produto ou null se não encontrado.</returns>
        public ItemsOrder? FindByProductId(int id) {
            ItemsOrder? itemsOrder = new();

            try {
                itemsOrder = context.ItemsOrders.FirstOrDefault(p => p.produtoId == id);
            } catch (Exception ex) {
                Console.WriteLine("Falha ao encontrar os items de pedido. " + ex.ToString());
            }

            return itemsOrder;
        }

        /// <summary>
        /// Adiciona uma lista de entidades ItemsOrder ao banco de dados.
        /// </summary>
        /// <param name="itemsOrder">A lista de ItemsOrder a ser adicionada.</param>
        /// <returns>True se os itens foram adicionados com sucesso, caso contrário, retorna false.</returns>
        public bool AddRangeItemsOrder(List<ItemsOrder> itemsOrder) {
            try {
                context.ItemsOrders.AddRange(itemsOrder);

                context.SaveChanges();

                return true;
            } catch (Exception ex) {
                Console.WriteLine("Falha ao adicionar os items de pedido. " + ex.ToString());
                return false;
            }
        }
    }
}
