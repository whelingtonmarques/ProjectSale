using System;
using System.Collections.Generic;
using System.Linq;
using ProjectSale.Data;
using ProjectSale.Entities;
using ProjectSale.Utils;
using ProjectSale.Respository;
using ProjectSale.Validations;
using System.Runtime.InteropServices;

namespace ProjectMarket {
    /// <summary>
    /// Classe principal do programa que contém o método Main e realiza interações com o usuário.
    /// </summary>
    public class Program : Utils {
        private static ProductsRepository _productsRepository;
        private static ItemsOrderRepository _itemsOrderRepository;
        private static OrderRepository _orderRepository;
        private static Validations _validations;

        static void Main(string[] args) {
            _productsRepository = new();
            _itemsOrderRepository = new();
            _orderRepository = new();
            _validations = new();

            var context = new AppDbContext();
            Console.WriteLine("************** BEM - VINDO **************");

            PrintMenu();

            while (true) {
                string? option = Console.ReadLine()?.ToLower();

                switch (option) {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        RemoveProduct(context);
                        break;
                    case "3":
                        ListProducts();
                        break;
                    case "4":
                        ChangeProductCategory(context);
                        break;
                    case "5":
                        CreateOrder(context);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Informe uma opção válida:");
                        break;
                }
            }
        }

        /// <summary>
        /// Adiciona um produto ao banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        static async void AddProduct() {
            // Solicita o nome e categoria do produto ao usuário
            Console.WriteLine("Informe o nome do produto:");
            string? name = Console.ReadLine();
            Console.WriteLine("Informe a categoria do produto: 1 = Perecível | 2 = Não Perecível");
            string? category = Console.ReadLine();

            try {

                var produto = new Product {
                    nome = name ?? string.Empty,
                    categoria = int.Parse(category ?? "0")
                };

                // Chama a função para adicionar o produto ao banco de dados
                bool isValid = await _productsRepository.AddProduct(produto);

                if (isValid) {
                    string[] msgs = {
                        "O produto foi adicionado.",
                        "---------------------------------",
                        ""
                    };

                    WriteIn(msgs);
                }
            } catch (Exception ex) {
                Console.WriteLine("Não foi possível adicionar o produto! " + ex.Message);
            }
        }

        /// <summary>
        /// Remove um produto do banco de dados, caso não exista nenhum pedido vinculado a ele.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        static void RemoveProduct(AppDbContext context) {
            Console.WriteLine("Informe o id do produto:");
            string? id = Console.ReadLine();

            if (int.TryParse(id, out int productId)) {
                try {
                    var product = _productsRepository.FindById(productId);

                    if (product != null) {
                        // Verifica se o produto está vinculado a algum pedido
                        var itemOrder = _itemsOrderRepository.FindByProductId(productId);

                        if (itemOrder != null) {
                            Console.WriteLine("Este produto não pode ser excluído pois está vinculado a um pedido.");
                        } else {
                            // Chama a função para remover o produto do banco de dados
                            bool success = _productsRepository.RemoveById(product);

                            if (success) {
                                string[] msgs =
                                {
                                    "Produto excluído com sucesso.",
                                    "---------------------------------",
                                    ""
                                };

                                WriteIn(msgs);
                            }
                        }
                    } else {
                        Console.WriteLine("Produto não encontrado.");
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Não foi possível excluir o produto! " + ex.Message);
                }
            } else {
                Console.WriteLine("Id inválido. Tente novamente.");
            }
        }

        /// <summary>
        /// Lista os produtos do banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        static void ListProducts() {
            WriteListProducts();
        }

        /// <summary>
        /// Altera a categoria de um produto no banco de dados, caso o produto não esteja vinculado a nenhuum pedido.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>
        static void ChangeProductCategory(AppDbContext context) {
            Console.WriteLine("Informe o id do produto:");
            string? id = Console.ReadLine();

            if (int.TryParse(id, out int productId)) {
                try {
                    var product = _productsRepository.FindById(productId);

                    if (product != null) {
                        // Verifica se o produto está vinculado a algum pedido
                        var itemOrder = _itemsOrderRepository.FindByProductId(productId);

                        if (itemOrder != null) {
                            Console.WriteLine("Este produto não pode ser alterado pois está vinculado a um pedido.");
                        } else {
                            // Solicita a nova categoria do produto ao usuário
                            Console.WriteLine("Informe a nova categoria do produto: 1 = Perecível | 2 = Não Perecível");
                            string? category = Console.ReadLine();

                            if (byte.TryParse(category, out byte newCategory)) {
                                product.categoria = newCategory;
                                // Chama a função para atualizar a categoria do produto no banco de dados
                                _productsRepository.UpdateProduct(product);

                                string[] msgs =
                                {
                                    "Categoria atualizada com sucesso.",
                                    "---------------------------------",
                                    ""
                                };

                                WriteIn(msgs);
                            } else {
                                Console.WriteLine("Categoria inválida. Tente novamente.");
                            }
                        }
                    } else {
                        Console.WriteLine("Produto não encontrado.");
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Não foi possível atualizar o produto! " + ex.Message);
                }
            } else {
                Console.WriteLine("Id inválido. Tente novamente.");
            }
        }

        /// <summary>
        /// Cria um novo pedido no banco de dados.
        /// </summary>
        /// <param name="context">O contexto do banco de dados.</param>

        static async void CreateOrder(AppDbContext context) {

            var newOrder = new Order();
            Console.WriteLine("Informe a descrição do pedido.");
            newOrder.descricao = Console.ReadLine() ?? string.Empty;

            bool success = await _orderRepository.CreateOrder(newOrder);

            if (success) {
                var currentOrder = _orderRepository.GetLastCreated();

                if (currentOrder == null) {
                    Console.WriteLine("Erro ao criar pedido.");
                    return;
                }

                // Exibe a lista de produtos disponíveis
                Console.WriteLine("Esses são os produtos disponíveis ->");
                WriteListProducts();

                var itemsOrder = new List<ItemsOrder>();

                bool addItems = true;
                int intem = 0;
                try {
                    do {
                        Console.WriteLine("Informe os dados do item " + intem++ + ":");

                        // Solicita os dados do item ao usuário
                        int productId = _validations.RequestProductId();
                        int quantity = _validations.RequestQuantity();
                        float value = _validations.RequestUniqueValue();

                        // Valida se o item já existe no pedido com o mesmo valor
                        bool valid = ValidateItems(itemsOrder, value, productId);

                        if (valid) {
                            var itemOrder = new ItemsOrder {
                                pedidoId = currentOrder.id,
                                produtoId = productId,
                                quantidade = quantity,
                                valor = value
                            };

                            //Calcula o valor total
                            currentOrder.valorTotal += CalculateTotalAmount(itemOrder);
                            itemsOrder.Add(itemOrder);

                            //Questiona se deseja adicionar outro intem ao pedido
                            addItems = _validations.RequestResponse();

                        } else {
                            Console.WriteLine("Produto já existe no pedido com este valor.");
                        }

                    } while (addItems);

                    // Chama as funções para adicionar uma lista de itens ao pedido no banco de dados
                    bool sucessAddRange = _itemsOrderRepository.AddRangeItemsOrder(itemsOrder);
                    bool successUpdate = _orderRepository.UpdateOrder(currentOrder);

                    if (sucessAddRange && successUpdate) {
                        context.SaveChanges();
                        Console.WriteLine("Pedido criado com sucesso.");
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Não foi possível criar o pedido! " + ex.Message);
                }
            }
        }
    }
}
