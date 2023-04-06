using ls.Services;
using Microsoft.AspNetCore.Mvc;

namespace ls.Controllers
{
    public class HomeController: Controller
    {
        private IRooms _rooms;
        public HomeController(IRooms rooms)
        {
            _rooms = rooms;
        }

        public IActionResult Index()
        {   
            return View(_rooms.rooms);
        }
    }
}
