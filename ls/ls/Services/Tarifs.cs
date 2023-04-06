using ls.Data;
using ls.Models;

namespace ls.Services
{
    public class Tarifs : ITarifs
    {
        private Repository _db;
        public Tarifs(Repository db)
        {
            _db = db;
        }

        List<Tarif> ITarifs.tarifs => _db.Tarifs.ToList();
    }
}
