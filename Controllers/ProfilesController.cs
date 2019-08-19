using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Debug;
using NetworkManager.Adapters;
using NetworkManager.Models;

namespace NetworkManager.Controllers
{
    public class ProfilesController : Controller
    {
        MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            var portProfiles = db.Profiles.ToList();
            return View(portProfiles);
        }

        public ActionResult Create()
        {
            return View(db.Vlans.ToList());
        }

        [HttpPost]
        public ActionResult CreateProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
            return RedirectToAction("Index", "Profiles");
        }
    }
}