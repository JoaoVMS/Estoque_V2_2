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
        public int Cod_Produto { get; set; }
        public int Qtd_Vendida { get; set; }

        public Vendas(int Cod_Pedido, int Cod_Produto, int Qtd_Vendida)
        {
            this.Cod_Pedido = Cod_Pedido;
            this.Cod_Produto = Cod_Produto;
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
            return "Cod_Pedido: " + Cod_Pedido + "; Cod_Produto: " + Cod_Produto + "; Qtd_Vendida: " + Qtd_Vendida; 
        }
        public void Inserir(IDado val) { }
    }
}
