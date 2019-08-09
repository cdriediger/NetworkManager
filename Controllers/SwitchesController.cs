using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Debug;
using NetworkManager.Adapters;
using NetworkManager.Models;

namespace NetworkManager
{
    public class SwitchesController : Controller
    {
        MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            var switches = db.Switches.ToList();
            foreach (var sw in switches) {
                if (sw.adapter is null) { sw.adapter = new HPE_Aruba_Adapter(sw.ipv4, sw.username, sw.password); }
                sw.modelName = db.SwitchModels.Where(s => s.Id == sw.model).First().Name;
                sw.name = sw.adapter.GetSystemName();
                sw.location = sw.adapter.GetLocation();
                sw.adapter.Close();
            }
            return View(switches);
        }

        public ActionResult Create()
        {
            return View(db.SwitchModels.ToList());
        }

        [HttpPost]
        public ActionResult CreateSwitch(Switches sw)
        {
            var portCount = db.SwitchModels.Where(s => s.Id == sw.model).First().PortCount;
            for (var id = 1; id <= portCount; id++) {
                var port = new Ports(){
                    name = id,
                    vlan = 1
                };
                sw.ports.Add(port);
            }
            db.Switches.Add(sw);
            db.SaveChanges();
            return RedirectToAction("Index", "Switches");
        }
        
        
        public ActionResult Delete(int id)
        {
            return View(db.Switches.Where(s => s.id == id).First());
        }

        [HttpPost]
        public ActionResult DeleteSwitch(int id)
        {
            try
            {
                Switches sw = db.Switches.Where(s => s.id == id).First();
                db.Switches.Remove(sw);
                db.SaveChanges();
                return RedirectToAction("Index", "Switches");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index", "Switches");
            }
            
        }
        
        public ActionResult Details(int id)
        {
            var sw = db.Switches.Where(s => s.id == id).First();
            if (sw.adapter is null) { sw.adapter = new HPE_Aruba_Adapter(sw.ipv4, sw.username, sw.password); }
            foreach (var port in sw.ports.ToList()) {
                port.state = sw.adapter.GetPortState(port.name);
                port.vlan = sw.adapter.GetPortVlan(port.name);
                port.description = sw.adapter.GetPortDescription(port.name);
            }
            var vlans = sw.adapter.GetVlans();
            sw.adapter.Close();
            return View(new DetailsViewModel(sw, vlans));
        }

        public ActionResult Update(int id)
        {
            return View(db.Switches.Where(s => s.id == id).First());
        }
        
        [HttpPost]
        public ActionResult UpdateSwitch(Switches form)
        {
            Switches sw = db.Switches.Where(s => s.id == form.id).First();
            if (sw.adapter is null) { sw.adapter = new HPE_Aruba_Adapter(sw.ipv4, sw.username, sw.password); }
            sw.name = form.name;
            sw.adapter.SetSystemName(form.name);
            sw.ipv4 = form.ipv4;
            sw.location = form.location;
            sw.adapter.SetLocation(form.location);
            db.SaveChanges();
            sw.adapter.Close();
            return RedirectToAction("Index", "Switches");
        }

        public ActionResult Ports(int id) 
        {
            return View(db.Switches.Where(s => s.id == id).First().ports);
        }

        [HttpPost]
        public ActionResult UpdatePort(Ports form)
        {
            Ports port = db.Ports.Where(p => p.id == form.id).First();
            Switches sw = db.Switches.Where(s => s.id == port.switchId).First();
            if (sw.adapter is null) { sw.adapter = new HPE_Aruba_Adapter(sw.ipv4, sw.username, sw.password); }
            port.description = form.description;
            sw.adapter.SetPortDescription(port.name, form.description);
            port.vlan = form.vlan;
            sw.adapter.SetPortVlan(port.name, form.vlan);
            System.Console.WriteLine($"Change VLAN of Port {port.name} to {form.vlan}");
            db.SaveChanges();
            sw.adapter.Close();
            return RedirectToAction("Index", "Switches");
        }
    }
}