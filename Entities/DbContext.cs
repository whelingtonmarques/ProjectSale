using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjectSale.Data
{
    /// <summary>
    /// Representa o contexto do aplicativo de banco de dados.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Os produtos armazenados no banco de dados.
        /// </summary>
        public DbSet<Product> Products { get; set; } = null!;

        /// <summary>
        /// Os itens de pedido armazenados no banco de dados.
        /// </summary>
        public DbSet<ItemsOrder> ItemsOrders { get; set; } = null!;

        /// <summary>
        /// Os pedidos armazenados no banco de dados.
        /// </summary>
        public DbSet<Order> Orders { get; set; } = null!;

        /// <summary>
        /// Configura a conexão com o banco de dados.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=127.0.0.1,1460;Database=market;User ID=sa;Password=KEKW***5588;Trusted_Connection=False; TrustServerCertificate=True;");
    }
}
