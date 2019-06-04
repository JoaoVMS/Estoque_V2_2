using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Estoque_V2_2
{
    class Program
    {
        static Lista Lista_Pedidos;
        static Lista[] Lista_Produtos; //Hash >> *vetor* de listas
        static Arvore Arvore_de_Produtos;
        static void Main(string[] args)
        {
            //Instanciando objetos
            Lista_Pedidos = new Lista();
            Lista_Produtos = new Lista[4]; // [0] Bebida, [1] Comida, [2] Escritorio, [3] Utensilios]
            for (int pos = 0; pos < Lista_Produtos.Length; pos++)
                Lista_Produtos[pos] = new Lista();
            Arvore_de_Produtos = new Arvore();

            //Leitura dos arquivos
            Ler_Dados_ARQ1();
            Ler_Dados_Vendas();

            bool rep = true;
            while (rep)
            {
                switch (Menu())
                {
                    case 1:
                        Console.WriteLine(Arvore_de_Produtos.Relartorio());//registrar o valor faturado bruto e o lucro líquido da empresa até o momento.                        
                        break;
                    case 2:
                        Console.WriteLine("falta implementar...");
                        break;
                    case 3:
                        Console.Write("Digite o nome do produto: ");
                        string nomeProduto = Console.ReadLine();
                        Console.WriteLine(mostrar_pedidos_produto(nomeProduto));
                        break;
                    case 4:
                        Console.WriteLine("Digite a categoria a ser impressa: \n0. Bebida \n1. Comida \n2. Escritório \n3. Utensilios");
                        int choice = int.Parse(Console.ReadLine());
                        Console.WriteLine(hash_buscar(choice));
                        break;
                    case 5:
                        rep = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida...");
                        break;
                }
                Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }         
        }
        static int Menu()
        {
            try
            {                
                Console.Clear();
                Console.Write("\t\t==MENU==" +
                "\n1. Registrar o valor faturado bruto e o lucro líquido da empresa até o momento;" +
                "\n2. Mostrar o produto de maior faturamento; " +
                "\n3. Mostrar todos os pedidos de um produto;" +
                "\n4. Mostrar todos os produtos de uma categoria;" +
                "\n5. Sair." +
                "\nOpção: ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        static void Ler_Dados_ARQ1()
        {
            string nome_Arquivo = "AEDprodutos.txt";
            IDado novo = null;

            if (!File.Exists(nome_Arquivo))
                Console.WriteLine("Arquivo {0} não existe!", nome_Arquivo);
            else
            {
                using (StreamReader entrada = new StreamReader(nome_Arquivo))
                { //  nome_Produto, categoria, margem_Lucro, preco_Custo, estoque_Incial, minimo_Estoque
                    while (!entrada.EndOfStream)
                    {
                        string[] info = entrada.ReadLine().Split(';');
                        switch (info[1])
                        {
                            case "1":
                                novo = new Bebida(info[0], Convert.ToDouble(info[2]),
                                    Convert.ToDouble(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5]));
                                break;
                            case "2":
                                novo = new Comida(info[0], Convert.ToDouble(info[2]),
                                    Convert.ToDouble(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5]));
                                break;
                            case "3":
                                novo = new Escritorio(info[0], Convert.ToDouble(info[2]),
                                    Convert.ToDouble(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5]));
                                break;
                            case "4":
                                novo = new Utensilio(info[0], Convert.ToDouble(info[2]),
                                    Convert.ToDouble(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5]));
                                break;
                            default:
                                novo = null;
                                break;
                        }
                        if (novo != null)
                        {
                            //Caso tenha sido instanciado corretamente, o produto será adicionado as estruturas de dados
                            Arvore_de_Produtos.Inserir(novo);
                            int lugar_adequado = novo.GetHashCode();
                            Lista_Produtos[lugar_adequado].Inserir(novo);
                        }

                    }
                }
            }
        }
        static void Ler_Dados_Vendas()
        {
            string nome_Arquivo = "AEDvendas.txt";

            //Verificar se arquivo existe
            if (!File.Exists(nome_Arquivo))
            {
                Console.WriteLine("Falha ao ler vendas porque o arquivo {0} não existe!", nome_Arquivo);
                return;
            }

            //fazer a leitura do arquivo
            StreamReader leituraVendas = new StreamReader(nome_Arquivo);

            //Leitura do arquivo
            while (!leituraVendas.EndOfStream)
            {
                //leitura do codigo e do numero de produtos
                string[] info = leituraVendas.ReadLine().Split(';');
                if (info.Length == 2)
                {
                    //info[0] -> codigo venda
                    int codigoVenda = Convert.ToInt32(info[0]);
                    //info[1] -> numero de itens
                    int quantidade_Itens = Convert.ToInt32(info[1]);

                    for (int pos = 0; pos < quantidade_Itens; pos++)
                    {
                        //leitura itens
                        info = leituraVendas.ReadLine().Split(';');
                        if (info.Length == 2)
                        {
                            //info[0] -> nome do produto
                            string nomeProduto = info[0];
                            //info[1] -> quantidade do produto vendido
                            int quantidadeVendida = Convert.ToInt32(info[1]);

                            //Buscando produto
                            Produto produto_procurado = new Produto(nomeProduto, 0, 0, 0, 0);
                            produto_procurado = (Produto)(Arvore_de_Produtos.Buscar(produto_procurado));

                            //Criando objeto venda
                            Vendas vendas = new Vendas(codigoVenda, nomeProduto, quantidadeVendida, produto_procurado.FaturamentoBruto(), produto_procurado.LucroLiquido());

                            //Inserindo produto
                            if (produto_procurado != null)  //verifica se produto existeS
                                produto_procurado.Lista_de_Vendas.Inserir(vendas); //registrar vendas, compostas por um ou mais produtos, e o valor faturado.
                            else
                                Console.WriteLine("Produto não foi encontrado.S");
                        }
                    }
                }
            }

            leituraVendas.Close();
        }

        //mostrar o produto de maior faturamento.
        //mostrar todos os produtos de uma categoria

        static string mostrar_pedidos_produto(string nomeProduto)
        {
            //Fazendo busca
            Produto produtoProcurado = (Produto)(Arvore_de_Produtos.Buscar(new Produto(nomeProduto)));

            //Verificando se existe
            if (produtoProcurado != null)
                return produtoProcurado.Lista_de_Vendas.CodPedidos();
            else
                return string.Format("O produto {0} não foi encontrado.", nomeProduto);
        }

        static string hash_buscar(int n)
        {
            IDado aux = null;
            switch (n)
            {
                case 0: //comida
                    aux = new Bebida(null);
                    break;
                case 1: //bebida
                    aux = new Comida(null);
                    break;
                case 2: //escritorio
                    aux = new Escritorio(null);
                    break;
                case 3: //utensilios
                    aux = new Utensilio(null);
                    break;
                default:
                    Console.WriteLine("Opção não existe...");
                    break;
            }
            int lugar = aux.GetHashCode();
            return Lista_Produtos[lugar].ToString();
        }
    }
}
