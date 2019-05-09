using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Elemento
    {
        public IDado meuDado { get; set; }
        public Elemento prox;
        public Elemento(IDado d)
        {
            meuDado = d;
            prox = null;
        }
    }
}
