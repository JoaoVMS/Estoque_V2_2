using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Produto : IDado
    {
        public string Nome_Produto { get; private set; }
        public double Margem_Lucro { get; set; }
        public double Preco_Custo { get; set; }
        public int Estoque_Inicial { get; set; }
        public int Minimo_Estoque { get; set; }
        public double Imposto { get; set; }
        public int NumeroDeVendas { get; set; }
        public double FaturamentoBrutoTotal => (FaturamentoBruto() * NumeroDeVendas);
        public Lista Lista_de_Vendas { get; set; } // Fila de vendas?

        public Produto(string Nome_Produto, double Margem_Lucro, double Preco_Custo, int Estoque_Inicial, int Minimo_Estoque)
        {
            this.Nome_Produto = Nome_Produto;
            this.Margem_Lucro = Margem_Lucro;
            this.Preco_Custo = Preco_Custo;
            this.Estoque_Inicial = Estoque_Inicial;
            this.Minimo_Estoque = Minimo_Estoque;

            Lista_de_Vendas = new Lista();
        }
        public Produto(string Nome_Produto)
        {
            this.Nome_Produto = Nome_Produto;
        }
        public bool Equals(Produto product)
        {
            return (this.Nome_Produto == product.Nome_Produto);
        }
        public int CompareTo(object obj)
        {
            Produto aux = (Produto)(obj);
            return string.Compare(this.Nome_Produto, aux.Nome_Produto);
        }
        public override string ToString()
        {
            StringBuilder auxString = new StringBuilder();
            Elemento aux = Lista_de_Vendas.prim.prox;

            auxString.AppendLine("Nome: " + Nome_Produto);
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
        public double FaturamentoBruto()
        {
            double preco_Lucro = Preco_Custo + (Preco_Custo * Margem_Lucro);
            return preco_Lucro + (preco_Lucro * Imposto);
        }        
        public double LucroLiquido()
        {
            return Preco_Custo * Margem_Lucro;
        }
    }
}
