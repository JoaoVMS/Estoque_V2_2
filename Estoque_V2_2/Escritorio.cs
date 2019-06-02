using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Escritorio : Produto
    {
        public Escritorio(/*int codigo_Produto,*/ string nome_Produto, double margem_Lucro, double preco_Custo, int estoque_Incial, int minimo_Estoque)
            : base(/*codigo_Produto,*/ nome_Produto, margem_Lucro, preco_Custo, estoque_Incial, minimo_Estoque)
        {
            Imposto = 0.5;
        }
        public override string ToString()
        {
            return string.Format("Cód.: {0}; Nome: {1}; Categoria: Material de Escritório; Margem de Lucro: {2}%; " +
                "\nPreço de Custo: {3}; Estoque Inicial: {4}; Minimo Estoque: {5}",
                Cod_Produto, Nome_Produto, Margem_Lucro, Preco_Custo, Estoque_Inicial, Minimo_Estoque);
        }
        public override int GetHashCode()
        {
            return 2;
        }
    }
}
