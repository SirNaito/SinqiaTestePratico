using System.Diagnostics;

namespace BibliotecaCategorias
{
    interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
        DateTime NextPaymentDate { get; }
    }

    class Trade : ITrade
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


    abstract class Category
    {
        public abstract string GetCategory(ITrade trade, DateTime referenceDate);
    }

    class ExpiredCategory : Category
    {
        public override string GetCategory(ITrade trade, DateTime referenceDate)
        {
            // Se a data do próximo pagamento estiver atrasada há mais de 30 dias
            if ((referenceDate - trade.NextPaymentDate).Days > 30)
            {
                return "EXPIRED";
            }
            return null;
        }
    }

    class HighRiskCategory : Category
    {
        public override string GetCategory(ITrade trade, DateTime referenceDate)
        {
            // Se o valor for superior a 1,000,000 e o cliente for do setor privado
            if (trade.Value > 1000000 && trade.ClientSector == "Private")
            {
                return "HIGHRISK";
            }
            return null;
        }
    }

    class MediumRiskCategory : Category
    {
        public override string GetCategory(ITrade trade, DateTime referenceDate)
        {
            // Se o valor for superior a 1,000,000 e o cliente for do setor público
            if (trade.Value > 1000000 && trade.ClientSector == "Public")
            {
                return "MEDIUMRISK";
            }
            return null;
        }
    }

    class PortfolioClassifier
    {
        private List<Category> categories = new List<Category>();

        public PortfolioClassifier()
        {
            // Adiciona as categorias existentes
            categories.Add(new ExpiredCategory());
            categories.Add(new HighRiskCategory());
            categories.Add(new MediumRiskCategory());
        }

        public void AddCategory(Category category)
        {
            categories.Add(category);
        }

        public void ClassifyTrades(List<ITrade> trades, DateTime referenceDate)
        {
            foreach (var trade in trades)
            {
                string category = null;

                foreach (var cat in categories)
                {
                    category = cat.GetCategory(trade, referenceDate);
                    if (category != null) break; // Se encontrar uma categoria, já não precisa verificar as outras
                }

                Console.WriteLine(category ?? "No category");
            }
        }
    }

}
