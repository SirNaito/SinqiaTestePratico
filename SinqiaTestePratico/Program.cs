using BibliotecaCategorias;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace TesteRisco
{
    class Program
    {
        static void Main()
        {
            // Leitura da entrada
            Console.WriteLine("Digite a Data de Referencia:");
            DateTime referenceDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);
            Console.WriteLine("Digite a Quantidade de Transações:");
            int n = int.Parse(Console.ReadLine());
            List<ITrade> trades = new List<ITrade>();

            for (int i = 0; i < n; i++)
            {   
                Console.WriteLine("Digite o valor, o setor e a data da "+(i+1)+"º operação:"); 
                var input = Console.ReadLine().Split();
                double value = double.Parse(input[0]);
                string clientSector = input[1];
                DateTime nextPaymentDate = DateTime.ParseExact(input[2], "MM/dd/yyyy", null);

                trades.Add(new Trade(value, clientSector, nextPaymentDate));
            }

            // Criação do classificador de portfólio
            PortfolioClassifier portfolioClassifier = new PortfolioClassifier();

            // Classificar as operações
            portfolioClassifier.ClassifyTrades(trades, referenceDate);
        }
    }

}
