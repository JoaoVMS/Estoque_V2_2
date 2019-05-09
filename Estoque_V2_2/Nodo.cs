using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Nodo
    {
        public IDado meuDado { get; set; }
        public Nodo esquerda { get; set; }
        public Nodo direita { get; set; }

        public Nodo(IDado dado)
        {
            meuDado = dado;
            esquerda = direita = null;
        }

        public int Grau()
        {
            //Saber quantos filhos existem
            if (esquerda != null)
                if (direita != null)
                    return 2;
                else
                    return -1; //sinal para saber se é um filho pra esquerda
            else if (direita != null)
                return 1; //sinal para saber se é um filho para a direita
            else
                return 0; // folha
        }

        public Nodo Antecessor()
        {
            Nodo aux = esquerda;

            while (aux.direita != null)
                aux = aux.direita;

            return aux;
        }
    }
}
