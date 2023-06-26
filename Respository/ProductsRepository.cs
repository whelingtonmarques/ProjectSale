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
    /// Classe de repositório para gerenciar entidades de produtos no banco de dados.
    /// </summary>
    public class ProductsRepository {
        private AppDbContext context = new();

        /// <summary>
        /// Adiciona um novo produto ao banco de dados.
        /// </summary>
        /// <param name="product">O novo produto a ser adicionado.</param>
        /// <returns>True se o produto foi adicionado com sucesso, caso contrário, retorna false.</returns>
        public async Task<bool> AddProduct(Product product) {
            try {
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return true;
            } catch (Exception ex) {
                Console.WriteLine("Falha ao adicionar o produto! " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Retorna uma lista de todos os produtos do banco de dados.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        public List<Product> GetAllProducs() {
            List<Product> products = new();

            try {
                var listProducts = context.Products.Select(x => new Product {
                    id = x.id,
                    nome = x.nome,
                    categoria = x.categoria,
                }).ToList();

                products = (List<Product>)listProducts;
            } catch (Exception ex) {
                Console.WriteLine("Falha ao listar os produtos. " + ex.ToString());
            }

            return products;
        }

        /// <summary>
        /// Retorna um produto pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>O produto associado ao ID ou null se não encontrado.</returns>
        public Product? FindById(int id) {
            Product? product = new();

            try {
                product = context.Products.FirstOrDefault(p => p.id == id);
            } catch (Exception ex) {
                Console.WriteLine("Falha ao encontrar o produto. " + ex.ToString());
            }

            return product;
        }

        /// <summary>
        /// Remove um produto pelo seu ID.
        /// </summary>
        /// <param name="productToRemove">O produto a ser removido.</param>
        /// <returns>True se o produto foi removido com sucesso, caso contrário, retorna false.</returns>
        public bool RemoveById(Product productToRemove) {
            try {
                context.Products.Remove(productToRemove);
                context.SaveChanges();
                return true;
            } catch (Exception ex) {
                Console.WriteLine("Falha ao deletar o produto. " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Atualiza um produto no banco de dados.
        /// </summary>
        /// <param name="productToUpdate">O produto a ser atualizado.</param>
        /// <returns>True se o produto foi atualizado com sucesso, caso contrário, retorna false.</returns>
        public bool UpdateProduct(Product productToUpdate) {
            try {
                context.Products.Update(productToUpdate);
                context.SaveChanges();

                return true;
            } catch (Exception ex) {
                Console.WriteLine("Falha ao atualizar o produto. " + ex.ToString());
                return false;
            }
        }
    }
}
