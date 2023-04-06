using ls.Models;

namespace ls.Services
{
    public interface IRooms
    {
        List<Room> rooms { get; }
        Room GetRoom(int roomId);
    }
}
