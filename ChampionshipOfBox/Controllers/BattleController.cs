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
            var s = battleService.GetBattles().ToList();
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
        public async Task AddNewBattle(CreateBattleReq battle) 
        {
            var s = await battleService.AddNewBattle(battle);
        }

        [HttpPost]
        public async Task EditBattle(ModifyBattleRequest modifyBattle)
        {
            var s = await battleService.EditBattle(modifyBattle);
        }

        [HttpPost]
        public async Task DeleteBattle(int id)
        {
            await battleService.DeleteBattle(id);
        }
    }
}