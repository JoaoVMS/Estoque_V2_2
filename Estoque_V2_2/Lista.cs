using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Lista
    {
        public Elemento prim { get; set; }
        public Elemento ult { get; set; }

        public Lista()
        {
            this.prim = new Elemento(null);
            this.ult = this.prim;
        }

        public void Inserir(IDado d)
        {
            Elemento novo = new Elemento(d);

            ult.prox = novo;
            ult = ult.prox;
        }

        public string Retirar()
        {
            if (this.Vazia())
                return null;
            Elemento aux = this.prim;
            while (aux.prox != null)
                aux = aux.prox;

            if (aux.prox == null)
                return null;

            Elemento retirar = aux.prox;
            aux.prox = retirar.prox;
            if (retirar == this.ult)
                this.ult = aux;
            else retirar.prox = null;

            return retirar.meuDado.ToString();
        }

        public override string ToString()
        {
            if (this.Vazia()) return "Categoria vazia";

            StringBuilder auxImpressao = new StringBuilder();
            Elemento atual = this.prim.prox;
            while (atual != null)
            {
                auxImpressao.AppendLine(atual.meuDado.ToString());
                atual = atual.prox;
            }

            return auxImpressao.ToString();
        }
        public double FaturamentoBruto()
        {
            double valor = 0;
            Elemento atual = this.prim.prox;
            while (atual != null)
            {
                Vendas aux = (Vendas)atual.meuDado;
                valor += aux.Faturamento;
                atual = atual.prox;
            }

            return valor;
        }
        public double LucroLiquido()
        {
            double valor = 0;
            Elemento atual = this.prim.prox;
            while (atual != null)
            {
                Vendas aux = (Vendas)atual.meuDado;
                valor += aux.Liquido;
                atual = atual.prox;
            }

            return valor;
        }
        public string CodPedidos()
        {            
            if (this.Vazia()) return "Produto não existe.";
            int cont = 0;
            StringBuilder auxImpressao = new StringBuilder();
            Elemento atual = this.prim.prox;
            Vendas aux = new Vendas();
            while (atual != null) // percorre todas os elementos da lista
            {
                aux = (Vendas)atual.meuDado;
                auxImpressao.AppendLine(aux.Cod_Pedido.ToString()); // adiciona o cod do pedido
                atual = atual.prox;
                cont++;// contador de pedidos
            }

            auxImpressao.AppendLine("O produto " + aux.Nome_Produto + " aparece em " + cont + " pedidos");
            return auxImpressao.ToString();
        }
        public bool Vazia()
        {
            return (prim == ult);
        }

    }
}
