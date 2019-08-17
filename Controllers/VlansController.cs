using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Debug;
using NetworkManager.Adapters;
using NetworkManager.Models;

namespace NetworkManager
{
    public class VlansController : Controller
    {
        MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            var vlans = db.Vlans.ToList();
            return View(vlans);
        }

        public ActionResult Update(int id)
        {
            return View(db.Vlans.Where(v => v.id == id).First());
        }
        
        [HttpPost]
        public ActionResult UpdateVlan(Vlan form)
        {
            Vlan vlan = db.Vlans.Where(v => v.id == form.id).First();
            vlan.name = form.name;
            db.SaveChanges();
            return RedirectToAction("Index", "Vlans");
        }
    }
}