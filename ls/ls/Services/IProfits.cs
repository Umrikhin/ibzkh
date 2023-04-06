using ls.Models;

namespace ls.Services
{
    public interface IProfits
    {
        List<Profit> profits { get; }
        string EditProfit(Profit profit);
        string DeleteProfit(Profit profit);
        int AddProfit(Profit profit);
        Profit GetProfit(int profitId);
        List<Profit> GetProfitByRoom(int IdRoom);
    }
}
