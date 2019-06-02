using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Produto : IDado
    {
        public int Cod_Produto { get; private set; }
        public string Nome_Produto { get; private set; }           
        public double Margem_Lucro { get; set; }
        public double Preco_Custo { get; set; }
        public int Estoque_Inicial { get; set; }
        public int Minimo_Estoque { get; set; }
        public double Imposto { get; set; }
        public Lista Lista_de_Pedidos { get; set; } // Fila de vendas?

        public Produto(/*int Cod_Produto,*/ string Nome_Produto, double Margem_Lucro, double Preco_Custo, int Estoque_Inicial, int Minimo_Estoque)
        {
            this.Cod_Produto = Cod_Produto;
            this.Nome_Produto = Nome_Produto;
            this.Margem_Lucro = Margem_Lucro;
            this.Preco_Custo = Preco_Custo;
            this.Estoque_Inicial = Estoque_Inicial;
            this.Minimo_Estoque = Minimo_Estoque;

            Lista_de_Pedidos = new Lista();
        }
        public bool Equals(Produto product)
        {
            return (this.Cod_Produto == product.Cod_Produto);
        }
        public int CompareTo(object obj)
        {
            Produto aux = (Produto)(obj);

            if (Cod_Produto < aux.Cod_Produto)
                return -1;
            else if (Cod_Produto > aux.Cod_Produto)
                return 1;
            else
                return 0;
        }
        public void Inserir( IDado pedido)
        {
            Lista_de_Pedidos.Inserir(pedido);
        }
        public override string ToString()
        {
            StringBuilder auxString = new StringBuilder();
            Elemento aux = Lista_de_Pedidos.prim.prox;

            auxString.AppendLine("Produto: " + Cod_Produto + "; Nome: " + Nome_Produto);
            auxString.AppendLine("Pedidos: ");

            while (aux != null)
            {
                auxString.AppendLine(aux.meuDado.ToString());
                aux = aux.prox;
            }

            return auxString.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public double Calcular_Valor_Venda()
        {
            double preco_Lucro = Preco_Custo + (Preco_Custo * Margem_Lucro);
            return preco_Lucro + (preco_Lucro * Imposto);
        }
    }
}
