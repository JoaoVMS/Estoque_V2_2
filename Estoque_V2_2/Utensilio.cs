using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Utensilio : Produto
    {
        public Utensilio(/*int codigo_Produto,*/ string nome_Produto, double margem_Lucro, double preco_Custo, int estoque_Incial, int minimo_Estoque)
            : base(/*codigo_Produto,*/ nome_Produto, margem_Lucro, preco_Custo, estoque_Incial, minimo_Estoque)
        {
            Imposto = 0.6;
        }
        public Utensilio(string nome_Produto) : base(nome_Produto)
        {

        }
        public override string ToString()
        {
            return string.Format("Nome: {0}; Categoria: Utensilio; Margem de Lucro: {1}%; " +
                "\nPreço de Custo: {2}; Estoque Inicial: {3}; Minimo Estoque: {4}",
                Nome_Produto, Margem_Lucro, Preco_Custo, Estoque_Inicial, Minimo_Estoque);
        }
        public override int GetHashCode()
        {
            return 3;
        }
    }
}
