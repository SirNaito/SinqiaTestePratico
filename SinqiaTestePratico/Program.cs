using BibliotecaCategorias;

namespace TesteRisco
{
    class Program
    {
        static void Main()
        {
            bool exitapp = false;
            while (exitapp != true)
            {
                // Inserção dos parametros

                //Data de Referencia
                Console.WriteLine("Digite a Data de Referencia (dd/MM/yyyy):");
                string referenceDateTemp = Console.ReadLine();
                DateTime referenceDate;
                while (!DateTime.TryParseExact(referenceDateTemp, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out referenceDate))
                {
                    Console.WriteLine("Data inválida, tente novamente ajustando valores ou adequando ao formato dd/MM/yyyy:");
                    referenceDateTemp = Console.ReadLine();
                }

                //Nº de Transações a verificar
                Console.WriteLine("Digite a Quantidade de Transações:");
                string ntemp = Console.ReadLine();
                int n;
                while (!int.TryParse(ntemp, out n))
                {
                    Console.WriteLine("Valor inválido, tente novamente:");
                    ntemp = Console.ReadLine();
                }

                List<ITrade> trades = new List<ITrade>();
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Digite o valor, o setor e a data (dd/MM/yyyy) da " + (i + 1) + "º operação:");
                    var input = Console.ReadLine().Split();

                    double value;
                    if (!double.TryParse(input[0], out value))
                    {
                        Console.WriteLine("Valor inválido, tente novamente:");
                        i--;
                        continue;
                    }

                    string clientSector = input[1];

                    DateTime nextPaymentDate;
                    if (!DateTime.TryParseExact(input[2], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out nextPaymentDate))
                    {
                        Console.WriteLine("Data inválida, tente novamente ajustando valores ou adequando ao formato dd/MM/yyyy:");
                        i--;
                        continue;
                    }

                    trades.Add(new Trade(value, clientSector, nextPaymentDate));
                }

                // Criação do classificador de portfólio
                PortfolioClassifier portfolioClassifier = new PortfolioClassifier();

                // Classificar as operações
                portfolioClassifier.ClassifyTrades(trades, referenceDate);

                //Pausa na operação
                Console.ReadLine();

                // Reiniciar aplicação
                Console.WriteLine("Deseja avaliar mais transações? (s/n):");
                string response = Console.ReadLine().ToLower();
                if (response != "s")
                {
                    exitapp = true;
                }
            }
        }
    }
}