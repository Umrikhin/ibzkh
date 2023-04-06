using ls.Models;

namespace ls.Services
{
    public class MockRooms : IRooms
    {
        private List<Room> data;

        public MockRooms() 
        {
            data = new List<Room>()
            {
                new Room() { Id = 1, NumBill="0001112223330001", Area=36.689, GAR="46a7ddb1-e7e1-4907-a942-54938826423c" },
                new Room() { Id = 2, NumBill="0001112223330002", Area=72.278, GAR="1afb33b8-e217-4a3d-8819-0a81495ab807" },
                new Room() { Id = 3, NumBill="0001112223330003", Area=41.509, GAR="ba4e473d-b83d-476f-834d-27c319be01aa" }
            };
        }

        public List<Room> rooms => data;

        public Room GetRoom(int roomId)
        {
            return data.Where(x => x.Id == roomId).FirstOrDefault();
        }
    }
}
