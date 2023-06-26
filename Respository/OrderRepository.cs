using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProjectSale.Data;
using ProjectSale.Entities;
using ProjectSale.Utils;

namespace ProjectSale.Respository {
    /// <summary>
    /// Classe de repositório para gerenciar entidades Order no banco de dados.
    /// </summary>
    public class OrderRepository : Utils.Utils {
        private AppDbContext context = new();

        /// <summary>
        /// Cria um novo pedido no banco de dados.
        /// </summary>
        /// <param name="newOrder">O novo pedido a ser criado.</param>
        /// <returns>True se o pedido foi criado com sucesso, caso contrário, retorna false.</returns>
        public async Task<bool> CreateOrder(Order newOrder) {
            try {
                newOrder.identificador = GenerateIdentifier();

                await context.Orders.AddAsync(newOrder);
                context.SaveChanges();

                return true;
            } catch (Exception ex) {
                Console.WriteLine("Falha ao encontrar os items de pedido. " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Retorna um pedido pelo seu identificador.
        /// </summary>
        /// <param name="identifier">O identificador do pedido.</param>
        /// <returns>O pedido associado ao identificador ou null se não encontrado.</returns>
        public Order? GetByIdentifier(string identidifier) {
            Order? order = new();

            try {
                return context.Orders.FirstOrDefault(i => i.identificador == identidifier);
            } catch (Exception ex) {
                Console.WriteLine("Falha ao encontrar os items de pedido. " + ex.ToString());
            }

            return null;
        }

        /// <summary>
        /// Retorna o último pedido criado no banco de dados.
        /// </summary>
        /// <returns>O último pedido criado ou null se não houver pedidos.</returns>
        public Order? GetLastCreated() {
            try {
                return context.Orders.ToList().LastOrDefault();
            } catch (Exception ex) {
                Console.WriteLine("Falha ao encontrar o pedido. " + ex.ToString());
            }

            return null;
        }

        /// <summary>
        /// Atualiza um pedido no banco de dados.
        /// </summary>
        /// <param name="order">O pedido a ser atualizado.</param>
        /// <returns>True se o pedido foi atualizado com sucesso, caso contrário, retorna false.</returns>
        public bool UpdateOrder(Order order) {
            try {
                context.Orders.Update(order);
                context.SaveChanges();

                return true;
            } catch (Exception ex) {
                Console.WriteLine("Falha ao atualizar o pedido. " + ex.ToString());
                return false;
            }
        }
    }
}
