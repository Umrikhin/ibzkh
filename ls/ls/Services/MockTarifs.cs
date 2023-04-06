using ls.Models;
using System.Diagnostics.Metrics;

namespace ls.Services
{
    public class MockTarifs : ITarifs
    {
        private List<Tarif> data;

        public MockTarifs()
        {
            data = new List<Tarif>()
            {
                new Tarif() { Id = 1, Name="Капремонт", Val = 12.5 }
            };
        }

        List<Tarif> ITarifs.tarifs => data;
    }
}
