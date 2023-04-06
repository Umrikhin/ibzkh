using ls.Models;
using System.Diagnostics.Metrics;

namespace ls.Services
{
    public class MockProfits : IProfits
    {
        private List<Profit> data;

        public MockProfits()
        {
            data = new List<Profit>()
            {
                new Profit() { Id = 1, Month=1, InBalance=300.898, Accrued=600.780, Pay=500.789, OutBalance=200.78, Year=2023, IdRoom=1 },
                new Profit() { Id = 2, Month=2, InBalance=0.001, Accrued=600.780, Pay=600.780, OutBalance=0.001, Year=2023, IdRoom=1 },
                new Profit() {Id = 3, Month=3, InBalance=0.001, Accrued=600.780, Pay=600.780, OutBalance=0.000, Year=2022, IdRoom=1 }
            };
        }

        public List<Profit> profits => data;

        public int AddProfit(Profit profit)
        {
            if (data.Where(x => x.Month == profit.Month && x.Year == profit.Year && x.IdRoom == profit.IdRoom).Count() > 0) throw new Exception("В выбранном году для указанного месяца уже добавлены начисления.");
            profit.Id = data.Max(p => p.Id) + 1;
            data.Add(profit);
            return profit.Id;
        }

        public string DeleteProfit(Profit profit)
        {
            string result = string.Empty;
            try
            {
                data.RemoveAll(x => x.Id == profit.Id);
            }
            catch (Exception ex) { result= ex.Message; }
            return result;
        }

        public string EditProfit(Profit profit)
        {
            string result = string.Empty;
            try
            {
                data.Where(p => p.Id == profit.Id).ToList().ForEach(x => { x.Month = profit.Month; x.InBalance = profit.InBalance; x.Accrued = profit.Accrued; x.Pay = profit.Pay; x.OutBalance = profit.OutBalance; x.Year = profit.Year; x.IdRoom = profit.IdRoom; });
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }

        public Profit GetProfit(int profitId)
        {
            return data.Where(x => x.Id == profitId).FirstOrDefault();
        }

        public List<Profit> GetProfitByRoom(int IdRoom)
        {
            return data.Where(x => x.IdRoom == IdRoom).ToList();
        }
    }
}
