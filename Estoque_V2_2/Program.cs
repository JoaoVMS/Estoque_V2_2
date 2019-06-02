using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Estoque_V2_2
{
    class Program
    {
        static Lista Lista_Pedidos;
        static Lista[] Lista_Produtos; //Hash >> *vetor* de listas
        static Arvore Arvore_de_Produtos;
        static void Main(string[] args)
        {
            Lista_Pedidos = new Lista();
            Lista_Produtos = new Lista[4]; // [0] Bebida, [1] Comida, [2] Escritorio, [3] Utensilios]
            Arvore_de_Produtos = new Arvore();
            Ler_Dados_ARQ1();
            
            


            Console.ReadKey();
        }
        static void Ler_Dados_ARQ1()
        {
            string nome_Arquivo = "abc1.txt";
            IDado novo = null;            

            if (!File.Exists(nome_Arquivo))
                Console.WriteLine("Arquivo {0} não existe!", nome_Arquivo);
            else
            {
                using (StreamReader entrada = new StreamReader(nome_Arquivo))
                { //  nome_Produto, categoria, margem_Lucro, preco_Custo, estoque_Incial, minimo_Estoque
                    string[] info = entrada.ReadLine().Split(';');

                    while (!entrada.EndOfStream)
                    {
                        switch (info[2])
                        {
                            case "1":
                                novo = new Bebida(/*Convert.ToInt32(info[0])*/ info[1], Convert.ToDouble(info[3]), 
                                    Convert.ToDouble(info[4]), Convert.ToInt32(info[5]), Convert.ToInt32(info[6]));
                               // Lista_Produtos[0].Inserir(novo);
                                Arvore_de_Produtos.Inserir(novo);
                                break;
                            case "2":
                                novo = new Comida(/*Convert.ToInt32(info[0]),*/ info[1], Convert.ToDouble(info[3]),
                                    Convert.ToDouble(info[4]), Convert.ToInt32(info[5]), Convert.ToInt32(info[6]));
                               // Lista_Produtos[1].Inserir(novo);
                                Arvore_de_Produtos.Inserir(novo);
                                break;
                            case "3":
                                novo = new Escritorio(/*Convert.ToInt32(info[0]),*/ info[1], Convert.ToDouble(info[3]),
                                    Convert.ToDouble(info[4]), Convert.ToInt32(info[5]), Convert.ToInt32(info[6]));
                               // Lista_Produtos[2].Inserir(novo);
                                Arvore_de_Produtos.Inserir(novo);
                                break;
                            case "4":
                                novo = new Utensilio(/*Convert.ToInt32(info[0]),*/ info[1], Convert.ToDouble(info[3]),
                                    Convert.ToDouble(info[4]), Convert.ToInt32(info[5]), Convert.ToInt32(info[6]));
                               // Lista_Produtos[3].Inserir(novo);
                                Arvore_de_Produtos.Inserir(novo);
                                break;
                            default:
                                break;
                        }

                        int lugar_adequado = novo.GetHashCode();
                        Lista_Produtos[lugar_adequado].Inserir(novo);
                    }
                }
            }
        }
        static void Ler_Dados_ARQ2()
        {
            string nome_Arquivo = "abc2.txt";
            IDado novo;

            if (!File.Exists(nome_Arquivo))
                Console.WriteLine("Arquivo {0} não existe!", nome_Arquivo);
            else
            {
                using (StreamReader entrada = new StreamReader(nome_Arquivo))
                { // cod_Pedido; Qtd_Produtos
                    string[] info = entrada.ReadLine().Split(';');

                    while (!entrada.EndOfStream)
                    {
                        novo = new Pedido(Convert.ToInt32(info[0]), Convert.ToInt32(info[1]));
                        Lista_Pedidos.Inserir(novo);
                    }
                }
            }
        }
        static void Ler_Dados_ARQ3()
        {
            string nome_Arquivo = "abc3.txt";
            IDado novo;
            string pedido;

            if (!File.Exists(nome_Arquivo))
                Console.WriteLine("Arquivo {0} não existe!", nome_Arquivo);
            else
            {
                using (StreamReader entrada = new StreamReader(nome_Arquivo))
                { // cod_Pedido; cod_Produto; Qtd_Vendida
                    string[] info = entrada.ReadLine().Split(';');

                    while (!entrada.EndOfStream)
                    {
                        pedido = info[0];

                        while(info[0] == pedido)
                        {
                            novo = new Vendas(Convert.ToInt32(info[0]), Convert.ToInt32(info[1]), Convert.ToInt32(info[2]));
                            Arvore_de_Produtos.Inserir(novo);
                        }
                    }
                }
            }
        }

        static void hash_buscar(int n)
        {
            //***Colocar no main***
            //Console.WriteLine("Digite a categoria a ser impressa: \n0. Bebida \n1. Comida \n2. Escritório \n3. Utensilios");
            //int choice = int.Parse(Console.ReadLine());
            //hash_buscar(choice);

            IDado aux = null;
            switch(n)
            {
                case 0: //comida
                    aux = new Comida(null, 0, 0, 0, 0);
                    break;
                case 1: //bebida
                    aux = new Bebida(null, 0, 0, 0, 0);
                    break;
                case 2: //escritorio
                    aux = new Escritorio(null, 0, 0, 0, 0);
                    break;
                case 3: //utensilios
                    aux = new Utensilio(null, 0, 0, 0, 0);
                    break;
                default:
                    Console.WriteLine("Opção não existe...");
                    break;
            }
            int lugar = aux.GetHashCode();
            Lista_Produtos[lugar].ToString();
        }
    }
}
