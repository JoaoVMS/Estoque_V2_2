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
           
            Nodo busca = new Nodo(dado);
            busca = BuscaRecursiva(busca, Raiz);

            if (busca != null)
                return busca.meuDado;
            else
                return null;
        }
        public IDado Retirar(IDado dado)
        {
            Nodo retirada = new Nodo(dado);
            RetiradaRec(retirada, Raiz, out Nodo aux); 
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
            else
                raiz.direita = InserirRecursivo(novo, raiz.direita); //procura uma raiz nula na direita

            return raiz;
        }

        #region Valor Liquido e Bruto, Faturamento total e Produto de maior Faturamento

        /// <summary>
        /// Relatório contendo as seguintes informações: Valor liquido, valor bruto, produto de maior faturamento e faturamento total
        /// </summary>
        /// <returns></returns>
        public string Relartorio()
        {
            var lines = new StringBuilder();
            string result;
            try
            {
                lines.Append($"Valor faturado bruto: {_faturamentoBruto(Raiz):0.##}");
                lines.Append($"\nLucro liquido: {_valorLiquido(Raiz):0.##}");
                lines.Append("\nProduto de maior faturamento: " + _produtoMaiorFat(Raiz, (Produto)Raiz.meuDado).ToString());
                lines.Append($"\nFaturamento total: {_lucroLiquido(Raiz):0.##}");
                result = lines.ToString();
            }
            catch (Exception e)
            {
                result = (e.Message);
            }

            return result;
        }

        #region Getters
        public double ValorBruto
        {
            get { return _faturamentoBruto(Raiz); }
        }
        public double ValorLiquido
        {
            get { return _valorLiquido(Raiz); }
        }
        public double FaturamentoTotal
        {
            get { return _lucroLiquido(Raiz); }
        }
        public Produto Produto_De_Maior_Faturmento
        {
            get { return _produtoMaiorFat(Raiz, (Produto)Raiz.meuDado); }
        }
        #endregion

        #region Métodos Recursivos
        /// <summary>
        /// Método recursivo para encontrar na árvore o produto de maior faturamento
        /// </summary>
        /// <param name="root">Raiz da árvore</param>
        /// <param name="produto">Produto auxiliar</param>
        /// <returns>produto de maior faturamento</returns>
        private Produto _produtoMaiorFat(Nodo root, Produto produto)
        {
            if (root == null)
            {
                return null;
            }

            var aux = (Produto)(root.meuDado);

            if (aux.FaturamentoBrutoTotal > produto.FaturamentoBrutoTotal)
                produto = aux;

            _produtoMaiorFat(root.esquerda, produto);
            _produtoMaiorFat(root.direita, produto);

            return produto;
        }
        /// <summary>
        /// Método recursivo para determinar o valor liquido total dos produtos
        /// </summary>
        /// <param name="root">Raiz da árvore</param>
        /// <returns>valor liquido total dos produtos</returns>
        private double _valorLiquido(Nodo root)
        {            
            if (root == null)
            {
                return 0;
            }
            double liquido = 0;
            var aux = (Produto)root.meuDado;
            liquido = aux.Lista_de_Vendas.LucroLiquido();

            liquido += _valorLiquido(root.esquerda);
            liquido += _valorLiquido(root.direita);

            return liquido;
        }
        /// <summary>
        /// Método recursivo para determinar o valor faturado bruto da empresa
        /// </summary>
        /// <param name="root">Raiz da árvore</param>
        /// <returns>valor bruto total dos produtos</returns>
        private double _faturamentoBruto(Nodo root)
        {            
            if (root == null)
            {
                return 0;
            }
            double bruto = 0;
            var aux = (Produto)root.meuDado;
            bruto = aux.Lista_de_Vendas.FaturamentoBruto();            

            bruto += _faturamentoBruto(root.esquerda);
            bruto += _faturamentoBruto(root.direita);

            return bruto;
        }
        /// <summary>
        /// Método recursivo para determinar o o lucro líquido da empresa
        /// </summary>
        /// <param name="root">Raiz da árvore</param>
        /// <returns>faturamento total</returns>
        private double _lucroLiquido(Nodo root)
        {
            double value = 0;
            if (root == null)
            {
                return 0;
            }

            var aux = (Produto)root.meuDado;
            value += (aux.FaturamentoBruto());

            value += _lucroLiquido(root.esquerda);
            value += _lucroLiquido(root.direita);

            return value;
        }
        #endregion

        #endregion

        private Nodo BuscaRecursiva(Nodo busca, Nodo raiz)
        {
            if (raiz == null)
                return null;

            if (busca.meuDado.CompareTo(raiz.meuDado) == 0)                           
                return raiz;            
                
            else if (busca.meuDado.CompareTo(raiz.meuDado) < 0)
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
