using ls.Data;
using ls.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ls.Services
{
    public class Profits : IProfits
    {
        private Repository _db;

        public Profits(Repository db)
        {
            _db = db;
        }
        public List<Profit> profits => _db.Profits.ToList();

        public int AddProfit(Profit profit)
        {
            if (_db.Profits.Where(x => x.Month == profit.Month && x.Year == profit.Year && x.IdRoom == profit.IdRoom).Count() > 0) throw new Exception("В выбранном году для указанного месяца уже добавлены начисления.");
            _db.Profits.Add(profit);
            _db.SaveChanges();
            return profit.Id;
        }

        public string DeleteProfit(Profit profit)
        {
            string result = string.Empty;
            try
            {
                _db.Profits.Remove(profit);
                _db.SaveChanges();
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }

        public string EditProfit(Profit profit)
        {
            string result = string.Empty;
            try
            {
                _db.Profits.Update(profit);
                _db.SaveChanges();
            }
            catch (Exception ex) { result = ex.Message; }
            return result;
        }

        public Profit GetProfit(int profitId)
        {
            return _db.Profits.Where(x => x.Id == profitId).FirstOrDefault();
        }

        public List<Profit> GetProfitByRoom(int IdRoom)
        {
            return _db.Profits.Where(x => x.IdRoom == IdRoom).ToList();
        }
    }
}
