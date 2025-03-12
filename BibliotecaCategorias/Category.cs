using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace BibliotecaCategorias
{

    public interface ITrade
    {
        double Value { get; } // Valor da Transação
        string ClientSector { get; } // Setor do Cliente (Publico ou Privado)
        DateTime NextPaymentDate { get; } // Data do próximo pagamento
    }

    //Interface para o Strategy Pattern substituindo o conceito de herança da classe abstrata
    public interface ICategoryStrategy
    {
        string GetCategory(ITrade trade, DateTime referenceDate);
    }

   public class Trade : ITrade
    {
        public double Value { get; private set; }
        public string ClientSector { get; private set; }
        public DateTime NextPaymentDate { get; private set; }

        public Trade(double value, string clientSector, DateTime nextPaymentDate)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
        }
    }

    class ExpiredCategoryStrategy : ICategoryStrategy
    {
        public string GetCategory(ITrade trade, DateTime referenceDate)
        {
            // Se a data do próximo pagamento estiver atrasada há mais de 30 dias baseado na data de referencia
            if ((referenceDate - trade.NextPaymentDate).Days > 30)
            {
                return "EXPIRED";
            }
            return null;
        }
    }

    class HighRiskCategoryStrategy : ICategoryStrategy
    {
        public string GetCategory(ITrade trade, DateTime referenceDate)
        {
            // Se o valor for superior a 1 milhão e o cliente for do setor privado
            if (trade.Value > 1000000 && trade.ClientSector == "Private")
            {
                return "HIGHRISK";
            }
            return null;
        }
    }

    class MediumRiskCategoryStrategy : ICategoryStrategy
    {
        public string GetCategory(ITrade trade, DateTime referenceDate)
        {
            // Se o valor for superior a 1 milhão e o cliente for do setor público
            if (trade.Value > 1000000 && trade.ClientSector == "Public")
            {
                return "MEDIUMRISK";
            }
            return null;
        }
    }

    public class PortfolioClassifier
    {
        private List<ICategoryStrategy> categoryStrategies = new List<ICategoryStrategy>();

        public PortfolioClassifier()
        {
            // Adiciona as estratégias existentes
            categoryStrategies.Add(new ExpiredCategoryStrategy());
            categoryStrategies.Add(new HighRiskCategoryStrategy());
            categoryStrategies.Add(new MediumRiskCategoryStrategy());
        }

        public void AddCategoryStrategy(ICategoryStrategy categoryStrategy)
        {
            categoryStrategies.Add(categoryStrategy);
        }

        public void ClassifyTrades(List<ITrade> trades, DateTime referenceDate)
        {
            foreach (var trade in trades)
            {
                string category = null;

                foreach (var strategy in categoryStrategies)
                {
                    category = strategy.GetCategory(trade, referenceDate);
                    if (category != null) break; // Se encontrar uma categoria, já não precisa verificar as outras
                }

                Console.WriteLine(category ?? "Unkown Category");
            }
        }
    }

}
