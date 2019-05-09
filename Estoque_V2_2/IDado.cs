using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    interface IDado
    {
        string ToString();
        bool Equals(object obj);
        int CompareTo(object obj);
        void Inserir(IDado val);
    }
}
