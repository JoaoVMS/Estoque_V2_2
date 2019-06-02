using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Vendas : IDado
    {
        public int Cod_Pedido { get; set; }
        public string Nome_Produto { get; set; }
        public int Qtd_Vendida { get; set; }

        public Vendas(int Cod_Pedido, string Nome_Produto, int Qtd_Vendida)
        {
            this.Cod_Pedido = Cod_Pedido;
            this.Nome_Produto = Nome_Produto;
            this.Qtd_Vendida = Qtd_Vendida;
        }
        public int CompareTo(object obj)
        {
            Vendas aux = (Vendas)(obj);

            if (Cod_Pedido < aux.Cod_Pedido)
                return -1;
            else if (Cod_Pedido > aux.Cod_Pedido)
                return 1;
            else
                return 0;
        }
        public override string ToString()
        {
            return "Código do Pedido: " + Cod_Pedido + "; Nome do Produto: " + Nome_Produto + "; Quantidade Vendida: " + Qtd_Vendida; 
        }
    }
}
