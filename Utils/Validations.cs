using Azure;
using ProjectSale.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSale.Validations {
    /// <summary>
    /// Classe responsável por fornecer métodos de validação utilizados no projeto.
    /// </summary>
    public class Validations {

        /// <summary>
        /// Solicita ao usuário a quantidade desejada e valida se é um valor válido.
        /// </summary>
        /// <returns>A quantidade desejada.</returns>
        public int RequestQuantity() {
            do {
                Console.WriteLine("Quantidade desejada: ");

                try {
                    int quantity = int.Parse(Console.ReadLine() ?? "0");

                    if (quantity > 0) {
                        return quantity;
                    } else {
                        Console.WriteLine("Quantidade deve ser mair que zero. Tente novamente.");
                    }
                } catch (Exception) {
                    Console.WriteLine("Valor inválido. Tente novamente.");
                }
            } while (true);
        }

        /// <summary>
        /// Solicita ao usuário o valor unitário e valida se é um valor válido.
        /// </summary>
        /// <returns>O valor unitário.</returns>
        public float RequestUniqueValue() {
            do {
                Console.WriteLine("Valor unitário: ");

                try {
                    float value = float.Parse(Console.ReadLine() ?? "0");

                    if (value > 0) {
                        return value;
                    } else {
                        Console.WriteLine("Valor deve ser mair que zero. Tente novamente.");
                    }
                } catch (Exception) {
                    Console.WriteLine("Valor inválido. Tente novamente.");
                }
            } while (true);
        }

        /// <summary>
        /// Solicita ao usuário o ID do produto e valida se é um valor válido.
        /// </summary>
        /// <returns>O ID do produto.</returns>
        public int RequestProductId() {
            do {
                Console.WriteLine("Informe o ID do produto que deseja adicionar: ");
                string? valueInput = Console.ReadLine();

                try {
                    return int.Parse(valueInput);
                } catch {
                    Console.WriteLine("ID do produto inválido. Tente novamente.");
                }
            } while (true);
        }

        /// <summary>
        /// Solicita ao usuário uma resposta (S/N) e valida se é um valor válido.
        /// </summary>
        /// <returns>True se a resposta for 'S', False se a resposta for 'N'.</returns>
        public bool RequestResponse() {
            do {
                Console.WriteLine("Deseja adicionar outro item ? (S/N)");
                string? valueInput = Console.ReadLine();

                try {
                    if (valueInput != null && valueInput.ToLower() is "s" or "n") {
                        return valueInput.ToLower() == "s";
                    }
                } catch {
                    Console.WriteLine("Valor inválido. Tente novamente!");
                }
            } while (true);
        }
    }
}
