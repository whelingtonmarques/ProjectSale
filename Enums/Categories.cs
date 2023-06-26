using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSale.Enums
{
    /// <summary>
    /// Enumeração que define as categorias de produtos disponíveis.
    /// </summary>
    public enum Categories
    {
        [Description("Perecível")]
        Perecivel = 1,
        [Description("Não Perecível")]
        NaoPerecivel = 2,          
    }
}
