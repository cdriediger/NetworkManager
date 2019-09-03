using System;
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
                UpdateSwitchData(sw);
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
            var portCount = db.SwitchModels.Where(s => s.id == sw.model).First().portCount;
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
            UpdateSwitchData(sw);
            var vlans = db.Vlans.ToList();
            var profiles = db.Profiles.ToList();
            return View(new SwitchDetailsViewModel(sw, vlans, profiles));
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

        public void UpdateSwitchData(Switches sw)
        {
            if (DateTime.Compare(sw.lastUpdate.AddSeconds(120), DateTime.Now) <= 0)
            {
                Console.WriteLine($"Updating Data of {sw.name}");
                sw.adapter = new HPE_Aruba_Adapter(sw.ipv4, sw.username, sw.password);
                sw.name = sw.adapter.GetSystemName();
                sw.location = sw.adapter.GetLocation();
                sw.lastUpdate = DateTime.Now;
                var vlanList = sw.adapter.GetVlans();
                foreach (var vlan in vlanList)
                {
                    CreateVlanIfNotExist(vlan.Key, vlan.Value);
                }
                var portNames = sw.adapter.GetAllPortNames();
                var portVlans = sw.adapter.GetAllPortVlans();
                var portStates = sw.adapter.GetAllPortStates();
                var lldpRemoteDevices = sw.adapter.GetAllLldpRemoteDevices();
                foreach (var port in sw.ports)
                {
                    port.description = portNames[port.name];
                    port.vlan = portVlans[port.name];
                    port.state = portStates[port.name];
                    port.lastUpdate = DateTime.Now;
                    var taggedVlans = sw.adapter.GetPortTaggedVlans(port.name);
                    foreach (var vlanId in taggedVlans)
                    {
                        Console.WriteLine($"VLAN {vlanId} tagged on {port.name}");
                        var vlan = new TaggedVlan(vlanId);
                        if (!(port.taggedVlans.Contains(vlan)))
                        {
                            port.taggedVlans.Add(vlan);
                        }
                    }
                    if (lldpRemoteDevices.ContainsKey(port.name))
                    {
                        var lldpRemoteDeviceName = lldpRemoteDevices[port.name]["system_name"];
                        var query = db.Switches.Where(s => s.name == lldpRemoteDeviceName);
                        if (query.Count() > 0)
                        {
                            port.lldpRemoteDeviceName = $"Uplink to {lldpRemoteDeviceName}";
                            port.isUplink = true;
                        }
                        else
                        {
                            port.lldpRemoteDeviceName = lldpRemoteDeviceName;
                        }
                    }
                    else
                    {
                        port.lldpRemoteDeviceName = "None";
                    }
                }
                db.SaveChanges();
                sw.adapter.Close();
            }
            else
            {
                Console.WriteLine($"Skipping Update of {sw.name}");
            }
        }

        public void CreateVlanIfNotExist(int id, string name)
        {
            var result = db.Vlans.Where(v => v.vlanId == id);
            if (result.Count() == 1)
            {
                Console.WriteLine($"VLAN {id} found. Name: {name}");
            }
            else
            {
                Console.WriteLine($"VLAN {id} not found in DB. Creating Vlan...");
                var vlan = new Vlan(id, name);
                db.Vlans.Add(vlan);
                db.SaveChanges();
            }
        }
    }
}