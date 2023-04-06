using ls.Models;
using ls.Services;
using Microsoft.AspNetCore.Mvc;

namespace ls.Controllers
{
    public class ProfitController : Controller
    {
        private ITarifs _tarifs;
        private IRooms _rooms;
        private IProfits _profits;
        public ProfitController(IRooms rooms, IProfits profits, ITarifs tarifs)
        {
            _rooms = rooms;
            _profits = profits;
            _tarifs = tarifs;
        }

        [HttpGet]
        public IActionResult Index(int Id)
        {
            var room = _rooms.GetRoom(Id);
            if (room == null) return RedirectToAction("Index", "Home");
            ViewBag.NumBill = room.NumBill;
            ViewBag.IdRoom = room.Id;
            var model = _profits.GetProfitByRoom(room.Id).OrderByDescending(y => y.Year).ThenByDescending(s => s.Month).Take(100).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(int IdRoom)
        {
            var room = _rooms.GetRoom(IdRoom);
            if (room == null) return RedirectToAction("Index", "Home");
            Profit model = new Profit(); //Создание новой модели
            model.Year = DateTime.Now.Year; //Установка периода
            model.IdRoom = IdRoom; //Назначение помещения
            var LastProfit = _profits.GetProfitByRoom(IdRoom);
            //Установка месяца и входящего баланса
            if (LastProfit.Count() > 0)
            {
                //Установка следующего месяца для начисления (предыдущий + 1)
                //Если превысит 12, то возвращаемся на текущий месяц
                model.Month = LastProfit.OrderByDescending(y => y.Year).ThenByDescending(s => s.Month).FirstOrDefault().Month + 1;
                if (model.Month > 12)
                {
                    model.Month = DateTime.Now.Month;
                }
                model.InBalance = LastProfit.OrderByDescending(y => y.Year).ThenByDescending(s => s.Month).FirstOrDefault().OutBalance;
            }
            else
            {
                model.Month = DateTime.Now.Month;
                model.InBalance = 0;
            }
            //Возврат тарифа по капремонту
            var tarif = _tarifs.tarifs.Where(x => x.Id == 1).FirstOrDefault().Val;
            //Расчет начисления
            model.Accrued = room.Area * tarif;
            //Установка значений по умолчанию для опланено и расчет исходящего баланса
            model.Pay = 0;
            model.OutBalance = model.Accrued - model.Pay.Value + model.InBalance;
            try
            {
                var newId = _profits.AddProfit(model);
            }
            catch (Exception ex)
            {
                TempData["errorCreate"] = ex.Message;
            }
            return RedirectToAction("Index", "Profit", new { Id = IdRoom });
        }

        [HttpGet]
        public IActionResult Edit(int id, int IdRoom)
        {
            var room = _rooms.GetRoom(IdRoom);
            if (room == null) return RedirectToAction("Index", "Home");
            ViewBag.NumBill = room.NumBill;
            ViewBag.IdRoom = IdRoom;
            var model = _profits.GetProfit(id);
            if (model == null) return RedirectToAction("Index", "Home");
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Profit model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Расчет исходящего сальдо после внесения оплаты
                    model.OutBalance = model.Accrued - model.Pay.Value + model.InBalance;
                    var error = _profits.EditProfit(model);
                    return RedirectToAction("Index", "Profit", new { Id = model.IdRoom });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

    }
}
