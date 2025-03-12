using System.Diagnostics;

namespace BibliotecaCategorias
{
    public interface ITrade
    {
        double Value { get; } //Valor da Transação
        string ClientSector { get; } //Setor do Cliente (Público ou Privado)
        DateTime NextPaymentDate { get; } //Data do Próximo Pagamento
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


    public abstract class Category
    {
        public abstract string GetCategory(ITrade trade, DateTime referenceDate);
    }

    class ExpiredCategory : Category
    {
        public override string GetCategory(ITrade trade, DateTime referenceDate)
        {
            // Se a data do próximo pagamento estiver atrasada há mais de 30 dias baseado na data de referencia
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
            // Se o valor for superior a 1 milhão e o cliente for do setor privado
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

                Console.WriteLine(category ?? "Unknown Category");

            }
        }
    }

}
