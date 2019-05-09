using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Pedido : IDado
    {
        public string ID { get; set; }
        public Fila Produtos_do_Pedido { get; set; }
        public Pedido(string ID, Fila Fila)
        {
            this.ID = ID;
            this.Produtos_do_Pedido = Fila;
        }
        public override string ToString()
        {
            StringBuilder auxString = new StringBuilder();
            Elemento aux = Produtos_do_Pedido.prim.prox;

            auxString.AppendLine("Pedido nº" + ID);

            while (aux != null)
            {
                auxString.AppendLine(aux.meuDado.ToString());
                aux = aux.prox;
            }

            return auxString.ToString();
        }
    }
}
