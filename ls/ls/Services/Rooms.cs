using ls.Data;
using ls.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ls.Services
{
    public class Rooms:IRooms
    {
        private Repository _db;

        public Rooms(Repository db)
        {
            _db = db;
        }

        public List<Room> rooms => _db.Rooms.ToList();

        public Room GetRoom(int roomId)
        {
            return _db.Rooms.Where(x => x.Id == roomId).FirstOrDefault();
        }
    }
}
