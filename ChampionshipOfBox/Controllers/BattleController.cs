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
    public class BattleController : Controller
    {
        BattleService battleService = new BattleService(); 

        // GET: Battle
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBattles()
        {
            var jsonData = new
            {
                rows = battleService.GetValidateBattles().ToList()
                //!note! - JSON can accept only and only such data type, do not change the name, accepted data type is only a string
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EditBattle(string oper, ModifyBattleRequest modifyBattle)
        {
            switch (oper)
            {
                case "add":
                    {
                        await battleService.AddNewBattle(modifyBattle);
                        return Content("true");
                    }
                case "edit":
                    {
                        await battleService.EditBattle(modifyBattle);
                        return Content("true");
                    }
                case "del":
                    {
                        await battleService.DeleteBattle(modifyBattle.Id);
                        return Content("true");
                    }
            }
            return Content("false");
        }
    }
}