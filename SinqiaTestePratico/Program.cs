using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace menu
{
    class Program
    {
        static void Main(string[] args)
        {
            int opt = 1;
            while (opt != 0)
            {
                Console.WriteLine("Olá, Selecione a opção desejada a seguir: \r");
                Console.WriteLine("----------------------------------------- \n");
                Console.WriteLine("1 - Comparar Riscos");
                Console.WriteLine("2 - Adicionar Categorias");
                Console.WriteLine("0 - Sair");
                opt = int.Parse(Console.ReadLine());

                if (opt == 0)
                {
                    break;
                }
                else if (opt == 1) 
                {
                    Console.WriteLine("Comparação de Riscos selecionada");
                }
                else if (opt == 1)
                {
                    Console.WriteLine("Adição de Categorias selecionada");
                }
                else
                {
                    Console.WriteLine("Opção inválida");
                }
            }

        }
    }
}
