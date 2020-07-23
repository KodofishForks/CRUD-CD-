using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.EfModels;

namespace WebApplication3.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {         
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult GameManagement()
        {
            BlockchainContext db = new BlockchainContext();
            var res = (from x in db.Game select x).ToList();
            ViewBag.games = res; //動態
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            BlockchainContext db = new BlockchainContext();
            var res = (from x in db.Game where x.Id == id select x).FirstOrDefault();
            if (res == null)
            {
                return Content("Miss data");
            }
            else
            {
                db.Game.Remove(res);
                db.SaveChanges();
                return Content("ok");
            }
        }
        public IActionResult AddGame(IFormCollection fc)
        {
            string name = fc["name"];
            string description = fc["description"];
            BlockchainContext db = new BlockchainContext();
            Game game = new Game
            {
                Name = name,
                Description = description
            };
            db.Game.Add(game);
            int number = db.SaveChanges();
            if (number == 1)
            {
                return RedirectToAction("GameManagement");
            }
            else
            {
                return View("Add");
            }

        }
    }
}
