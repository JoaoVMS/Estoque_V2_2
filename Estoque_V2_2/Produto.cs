using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Produto : IDado
    {
        public string ID { get; private set; }
        public string NomeProduto { get; private set; }
        public double PrecoCusto { get; set; }
        public double MargemLucroMIN { get; set; }
        public double MargemLucroMAX { get; set; }
        public double Imposto { get; set; }
        public Fila Fila_de_Pedidos { get; set; }

        public Produto(string ID, string NomeProduto, double PrecoCusto, double MargemLucroMIN, double MargemLucroMAX)
        {
            this.ID = ID;
            this.NomeProduto = NomeProduto;
            this.PrecoCusto = PrecoCusto;
            this.MargemLucroMIN = MargemLucroMIN;
            this.MargemLucroMAX = MargemLucroMAX;
            Fila_de_Pedidos = new Fila();
        }
        public bool Equals(Produto product)
        {
            return (this.ID == product.ID);
        }
    }
}
