using ProjectSale.Data;
using ProjectSale.Entities;
using ProjectSale.Enums;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using ProjectSale.Respository;

namespace ProjectSale.Utils {

    /// <summary>
    /// Classe contendo métodos utilitários para o projeto de vendas.
    /// </summary>
    public class Utils {

        /// <summary>
        /// Obtém o nome da categoria com base no valor da enumeração de categoria.
        /// </summary>
        /// <param name="category">Valor da categoria.</param>
        /// <returns>O nome da categoria.</returns>
        public static string GetCategoryName(int category) {
            switch (category) {
                case (int)Categories.Perecivel:
                    return Categories.Perecivel.ToString();
                case (int)Categories.NaoPerecivel:
                    return Categories.NaoPerecivel.ToString();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Exibe o menu de opções do projeto de vendas no console.
        /// </summary>
        public static void PrintMenu() {
            string[] msgs =
            {
            "Escolha uma opção:",
            "1    - Adicionar produto",
            "2    - Excluir produto",
            "3    - Listar produtos",
            "4    - Alterar categoria do produto",
            "---------------------------------",
            "5    - Criar pedido",
            "Exit - Sair"
        };

            WriteIn(msgs);
        }

        /// <summary>
        /// Escreve as mensagens no console, uma por linha.
        /// </summary>
        /// <param name="msgs">As mensagens a serem escritas.</param>
        public static void WriteIn(string[] msgs) {
            foreach (string msg in msgs) {
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// Escreve a lista de produtos no console em formato de tabela.
        /// </summary>
        public static void WriteListProducts() {
            ProductsRepository _productsRepository = new();

            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - -");
            try {
                var products = _productsRepository.GetAllProducs();
                var table = new ConsoleTable("ID", "Nome", "Categoria");

                foreach (var product in products) {
                    table.AddRow(product.Id, product.Nome, GetCategoryName(product.Categoria));
                }

                table.Write();
                Console.WriteLine(" - - - - - - - - - - - - - - - - - - - -");
            } catch (Exception ex) {
                Console.WriteLine("Não foi possível listar os produtos. " + ex.ToString());
            }
        }

        /// <summary>
        /// Gera um identificador único para um pedido.
        /// </summary>
        /// <returns>O identificador gerado.</returns>
        public static string GenerateIdentifier() {
            var random = new Random();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numbers = "0123456789";

            var letter = letters[random.Next(letters.Length)];
            var number = new string(Enumerable.Repeat(numbers, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"P_{letter}{number}_C";
        }

        /// <summary>
        /// Calcula o valor total de um item de pedido.
        /// </summary>
        /// <param name="itemOrder">O item de pedido.</param>
        /// <returns>O valor total calculado.</returns>
        public static decimal CalculateTotalAmount(ItemsOrder itemOrder) {
            return (decimal)(itemOrder.Quantidade * itemOrder.Valor);
        }

        /// <summary>
        /// Valida se um item de pedido é único com base no valor e ID fornecidos.
        /// </summary>
        /// <param name="itemOrder">A lista de itens de pedido.</param>
        /// <param name="uniqueValue">O valor a ser verificado.</param>
        /// <param name="idToAdd">O ID a ser adicionado.</param>
        /// <returns>True se o item for único, False caso contrário.</returns>
        public static bool ValidateItems(List<ItemsOrder> itemOrder, float? uniqueValue, int? idToAdd) {
            return itemOrder?.FirstOrDefault(p => p.ProdutoId == idToAdd && p.Valor == uniqueValue) == null;
        }


    }
}