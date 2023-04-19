using System;
using System.Diagnostics;
using static System.Console;
using System.Text;
using System.IO;

namespace MyNamespace
{
    class MainClass
    {

        public static void printMenu(String[] options)
        {
            foreach (String option in options)
            {
                WriteLine(option);
            }
            WriteLine("Escolha a sua opção: ");
        }

        public static void Main(String[] args)
        {

            WriteLine(">>>>>>CADASTRO DE CLIENTES<<<<<<");
            String[] options = { "1- Cadastrar",
                                 "2- Editar",
                                 "3- Excluir",
                                 "4- Listar",
                                 "5- Gravar",
                                 "6- Ler",
                                 "7- Sair"};
            int option = 0;
            while (true)
            {
                printMenu(options);
                try
                {
                    option = Convert.ToInt32(ReadLine());
                }
                catch (FormatException)
                {
                    WriteLine($"Digite uma opção entre 1 e {options.Length}: ");
                    continue;
                }
                catch (Exception)
                {
                    WriteLine("Ocorreu um erro.");
                    continue;
                }
                switch (option)
                {
                    case 1:
                        Cadastrar();
                        break;
                    case 2:
                        Editar();
                        break;
                    case 3:
                        Excluir();
                        break;
                    case 4:
                        Listar();
                        break;
                    case 5:
                        Gravar();
                        break;
                    case 6:
                        Ler();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    default:
                        WriteLine($"Digite uma opção entre 1 e {options.Length}");
                        break;
                }
            }
        }
        static List<string> nomes = new List<string>();
        static List<int> idades = new List<int>();

        private static void Cadastrar()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine("...INICIANDO CADASTRO...");
            ResetColor();
            WriteLine("Digite o nome: ");
            string nome = ReadLine();
            var repetido = nomes.Any(x => x.Contains(nome));  // recebe true na var se na lista tiver um nome ja digitado
            if (repetido == true)
            {
                WriteLine("Essa pessoa ja existe!!\n");
                return;
            }
            else
            {
                nomes.Add(nome);
            }
            WriteLine("Digite a idade: ");
            idades.Add(Convert.ToInt32(ReadLine()));
            WriteLine();
        }
        private static void Listar()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine("\n....LISTAGEM DE PESSOAS....");
            ResetColor();
            int pos = 0;
            foreach (var item in nomes)
            {
                WriteLine($"Nome: {item} ||| idade: {idades[pos]}");
                pos++;
            }
            WriteLine();
        }
        private static void Editar()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine(">>>>EDIÇAO DE CADASTRO<<<<<<\n");

            ResetColor();
            string nome = "";
            WriteLine("Digite o nome que deseja editar: ");
            nome = ReadLine();
            int index = nomes.IndexOf(nome); // vai procurar a posiçao do nome que o usuario digitou, na lista "nomes"
            if (index <= 0)
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine(">>>>CADASTRO QUE SERA EDITADO<<<<");
                ResetColor();
                WriteLine($"Nome: {nomes[index]}");
                WriteLine($"Idade: {idades[index]}\n");
                WriteLine("Digite o novo nome: ");
                nomes[index] = ReadLine();
                WriteLine("Digite a nova idade: ");
                idades[index] = Convert.ToInt32(ReadLine());
                ForegroundColor = ConsoleColor.Green;
                WriteLine("Cadastro editado!!");
                ResetColor();

            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Registro nao encontrado!!");
                ResetColor();
            }
        }
        private static void Excluir()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine(">>>>EXCLUSÂO DE CADASTRO<<<<");
            string nome = "";
            WriteLine("Digite o nome que deseja excluir");
            nome = ReadLine();
            int index = nomes.IndexOf(nome); // vai procurar a posiçao do nome que o usuario digitou, na lista "nomes"
            if (index <= 0)
            {
                WriteLine(">>>>CADASTRO QUE SERA EXCLUIDO<<<<");
                ResetColor();
                WriteLine($"Nome: {nomes[index]}");
                WriteLine($"Idade: {idades[index]}\n");
                nomes.RemoveAt(index);
                idades.RemoveAt(index);
                ForegroundColor = ConsoleColor.Green;
                WriteLine("Cadastro excluido com sucesso!!");
                ResetColor();

            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Cadastro nao encontrado!!");
                ResetColor();
            }
        }

        private static void Gravar()
        {
            WriteLine("\n>>>>GRAVANDO OS DADOS<<<<");
            try
            {
                StreamWriter dados;
                string arq = @"C:\Users\Aluno\source\repos\ConsoleApp1\dados.txt";
                dados = File.CreateText(arq);
                foreach (var item in nomes)
                {
                    dados.WriteLine($"{item}");
                }
                dados.Close();
                StreamWriter dados2;
                string arq2 = @"C:\Users\Aluno\source\repos\ConsoleApp1\dados2.txt";
                dados2 = File.CreateText(arq2);
                foreach (var item2 in idades)
                {
                    dados2.WriteLine($"{item2}");

                }
                dados2.Close();
                WriteLine();
            }
            catch (Exception e)
            {
                WriteLine($"Erro: {e.Message}");

            }
            finally
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("DADOS GRAVADOS COM SUCESSO!!!");
                ResetColor();
            }

        }

        private static void Ler()
        {
            Clear();
            WriteLine("\n>>>>LENDO ARQUIVO<<<<\n");
            var nome = File.ReadAllLines(@"C:\Users\Aluno\source\repos\ConsoleApp1\dados.txt"); 
            //o arquivo vai ler todas as linhas e ficar com ela
            for (int i = 0; i < nome.Length; i++)
            {
                nomes.Add(nome[i]);
            }
            var idade = File.ReadAllLines(@"C:\Users\Aluno\source\repos\ConsoleApp1\dados2.txt");
            for (int x = 0; x < idade.Length; x++)
            {
                idades.Add(Convert.ToInt32(idade[x]));
            }
            ForegroundColor = ConsoleColor.Green;
            WriteLine("\n>>>>LEITURA DE DADOS CONCLUIDA<<<<\n");
            ResetColor();
        }
    }
}