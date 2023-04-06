using ls.Models;
using ls.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;

namespace ls.Controllers
{
    public class StatusController : Controller
    {
        private IRooms _rooms;
        private IProfits _profits;
        public StatusController(IRooms rooms, IProfits profits)
        {
            _rooms = rooms;
            _profits = profits;
        }

        public IActionResult Index(int Id, int? Period)
        {
            var room = _rooms.GetRoom(Id);
            if (room == null) return RedirectToAction("Index", "Home");
            ViewBag.NumBill = room.NumBill;
            ViewBag.IdRoom = room.Id;
            if (Period == null) Period = DateTime.Now.Year;
            ViewBag.Years = GetYearForSelect(Period.Value);
            var model = _profits.GetProfitByRoom(room.Id).Where(x => x.Year == Period).OrderByDescending(s => s.Year).ThenBy(z => z.Month).Take(12).ToList();
            return View(model);
        }

        Microsoft.AspNetCore.Mvc.Rendering.SelectList GetYearForSelect(int selItem)
        {
            List<Period> items = new List<Period>();
            for (int y = DateTime.Now.Year; y >= DateTime.Now.AddYears(-10).Year; y--)
            {
                items.Add(new Period() { Val = y, Name = y.ToString() });
            }
            var selList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(items, "Val", "Name");
            selList.Where(x => x.Value == selItem.ToString()).ToList().ForEach(z => { z.Selected = true; });
            return selList;
        }

        //Получение данных по состоянию счета за период (выбранный год)
        [HttpPost]
        public IActionResult GetDataStatus(int Id, int Period = 0)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    return RedirectToAction("Index", "Status", new { Id = Id, Period = Period });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Years = GetYearForSelect(Period == 0 ? DateTime.Now.Year : Period);
            return View("Index");
        }
    }
}
