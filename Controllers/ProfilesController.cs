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
            var vlans = db.Vlans.ToList();
            return View(new ProfileIndexViewModel(portProfiles ,vlans));
        }

        public ActionResult Create()
        {
            return View(db.Vlans.ToList());
        }

        [HttpPost]
        public ActionResult CreateProfile(Profile profile)
        {
            
            profile.taggedVlans = new TaggedVlans();
            foreach (int vlanId in profile.TaggedVLanIds)
            {
                profile.taggedVlans.Add(new TaggedVlan(vlanId));
            }
            db.Profiles.Add(profile);
            db.SaveChanges();
            return RedirectToAction("Index", "Profiles");
        }

        public ActionResult Delete(int id)
        {
            return View(db.Profiles.Where(s => s.id == id).First());
        }

        [HttpPost]
        public ActionResult DeleteProfile(int id)
        {
            Console.WriteLine($"Delete Profile {id}");
            //try
            //{
                Profile profile = db.Profiles.Where(p => p.id == id).First();
                foreach (var TaggedVlan in profile.taggedVlans)
                {
                    db.TaggedVlans.Remove(TaggedVlan);
                }
                db.Profiles.Remove(profile);
                db.SaveChanges();
                return RedirectToAction("Index", "Profiles");
            //}
            //catch (System.Exception)
            //{
            //    Console.WriteLine($"failed to delete profile {id}");
            //    return RedirectToAction("Index", "Profiles");
            //}
            
        }
    }
}