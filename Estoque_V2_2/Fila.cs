using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Fila
    {
        public Elemento prim { get; set; }
        public Elemento ult { get; set; }

        public Fila()
        {
            prim = new Elemento(null);
            ult = prim;
        }

        public void Inserir(IDado d)
        {
            Elemento novo = new Elemento(d);

            ult.prox = novo;
            ult = novo;
        }

        public string Retirar()
        {
            if (Vazia())
                return null;

            Elemento aux = prim.prox;
            prim.prox = aux.prox;

            if (aux == ult)
                ult = prim;
            else
                aux.prox = null;

            aux.prox = null;

            return aux.meuDado.ToString();
        }

        //public override string ToString()
        //{
        //    if (Vazia())
        //        return null;

        //    StringBuilder auxString = new StringBuilder();
        //    Elemento aux = prim.prox;

        //    while (aux != null)
        //    {
        //        auxString.AppendLine(aux.meuDado.ToString());
        //        aux = aux.prox;
        //    }

        //    return auxString.ToString();
        //}

        public bool Vazia()
        {
            return (prim == ult);
        }
    }
}
