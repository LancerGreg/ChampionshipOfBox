using ChampionshipOfBox.Models;
using ChampionshipOfBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChampionshipOfBox.Controllers
{
    public class BoxController : Controller
    {
        BoxService service = new BoxService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MainPage(string name)
        {
            return PartialView(service.Rankings(name).ToList());
        }

        [HttpGet]
        public ActionResult Championship(Boxer boxer, bool result)
        {
            ViewBag.Championships = service.Championships(boxer, result);
            return View();
        }

        [HttpPut]
        public async Task<string> EditBattle(Battle editBattle)
        {
            await service.EditBattle(editBattle);
            return "The Battle has been edit";
        }

        [HttpPost]
        public async Task<string> AddNewBattle(Battle newBattle)
        {
            await service.AddNewBattle(newBattle);
            return "A new battle has been registered";
        }
    }
}