using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_V2_2
{
    class Arvore
    {
        public Nodo Raiz { get; set; }

        public Arvore()
        {
            this.Raiz = null;
        }

        public void Inserir(IDado dado)
        {
            Nodo aux = new Nodo(dado);
            this.Raiz = InserirRecursivo(aux, Raiz);
        }
        public IDado Buscar(IDado dado)
        {
            //IDado dado = new Numero(chave); // = new (Tipo da classe) (chave);
            Nodo busca = new Nodo(dado);

            return BuscaRecursiva(busca, Raiz).meuDado;
        }
        public IDado Retirar(IDado dado)
        {
            //IDado dado = new Numero(chave);
            Nodo retirada = new Nodo(dado);

            RetiradaRec(retirada, Raiz, out Nodo aux); //declaração dentro dos parametros do método

            return aux.meuDado;
        }

        public override string ToString()
        {
            return EmOrdem(Raiz);
        }

        private Nodo RetiradaRec(Nodo quem, Nodo onde, out Nodo saida)
        {
            if (onde == null)
            {
                saida = new Nodo(null);
                return null;
            }

            if (quem.meuDado.CompareTo(onde.meuDado) < 0)
                onde.esquerda = RetiradaRec(quem, onde.esquerda, out saida);
            else if (quem.meuDado.CompareTo(onde.meuDado) > 0)
                onde.direita = RetiradaRec(quem, onde.direita, out saida);
            else
            {
                saida = new Nodo(onde.meuDado);
                int grau = onde.Grau();

                switch (grau)
                {
                    case 0: //não possui filhos
                        return null;
                    case -1: //filho a esquerda
                        return onde.esquerda;
                    case 1: //filho a direita
                        return onde.direita;
                    case 2: //tem dois filhos
                        Nodo antecessor = onde.Antecessor();
                        onde.meuDado = antecessor.meuDado;
                        onde.esquerda = RetiradaRec(antecessor, onde.esquerda, out saida);
                        break;
                }
            }
            return onde;
        }
        private Nodo InserirRecursivo(Nodo novo, Nodo raiz)
        {
            if (raiz == null) //quando encontra uma raiz nula, vc insere novo
                return novo;

            if (novo.meuDado.CompareTo(raiz.meuDado) < 0) //procura uma raiz nula na esquerda
                raiz.esquerda = InserirRecursivo(novo, raiz.esquerda);
            else if (novo.meuDado.CompareTo(raiz.meuDado) == 0) // adiciona uma venda a fila do produto
            {
                IDado venda = novo.meuDado;
                raiz.meuDado.Inserir(venda);
            }
            else
                raiz.direita = InserirRecursivo(novo, raiz.direita); //procura uma raiz nula na direita

            return raiz;
        }        
        private Nodo BuscaRecursiva(Nodo busca, Nodo raiz)
        {
            if (raiz == null)
                return null;

            if (busca.meuDado.CompareTo(raiz) == 0)                           
                return raiz;
            
                
            else if (busca.meuDado.CompareTo(raiz) < 0)
                return BuscaRecursiva(busca, raiz.esquerda);
            else
                return BuscaRecursiva(busca, raiz.direita);
        }        
        private string EmOrdem(Nodo raiz)
        {
            if (raiz != null)
            {
                StringBuilder auxImpressao = new StringBuilder();

                auxImpressao.Append(EmOrdem(raiz.esquerda));
                auxImpressao.Append(raiz.meuDado.ToString());
                auxImpressao.Append(EmOrdem(raiz.direita));

                return auxImpressao.ToString();
            }
            else
                return null;
        }

        private string PreOrdem(Nodo raiz)
        {
            if (raiz != null)
            {
                StringBuilder auxImpressao = new StringBuilder();
                auxImpressao.Append(raiz.meuDado.ToString());
                auxImpressao.Append(PreOrdem(raiz.esquerda));
                auxImpressao.Append(PreOrdem(raiz.direita));

                return auxImpressao.ToString();
            }
            else
                return null;
        }

        private string PosOrdem(Nodo raiz)
        {            
            if (raiz != null)
            {
                StringBuilder auxImpressao = new StringBuilder();

                auxImpressao.Append(PosOrdem(raiz.direita));
                auxImpressao.Append(PosOrdem(raiz.esquerda));
                auxImpressao.Append(raiz.meuDado.ToString());

                return auxImpressao.ToString();
            }
            else
                return null;
        }
    }
}
