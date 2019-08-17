using System.Collections.Generic;
using NetworkManager.Models;

namespace NetworkManager.Models
{
    public class SwitchDetailsViewModel
    {
        public Switches sw {get; set;}
        public Dictionary<int, string> vlans {get; set;}

        public SwitchDetailsViewModel(Switches sw, List<Vlan> vlans)
        {
            this.sw = sw;
            var vlanList = new Dictionary<int,string>();
            foreach (var vlan in vlans)
            {
                vlanList.Add(vlan.id, vlan.name);
            }
            this.vlans = vlanList;
        }
    }
}