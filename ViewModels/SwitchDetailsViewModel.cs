using System.Collections.Generic;
using NetworkManager.Models;

namespace NetworkManager.Models
{
    public class SwitchDetailsViewModel
    {
        public Switches sw { get; set; }
        public Dictionary<int, string> vlans { get; set; }
        public Dictionary<int, string> profiles { get; set; }

        public SwitchDetailsViewModel(Switches sw, List<Vlan> vlans, List<Profile> profiles)
        {
            this.sw = sw;
            this.vlans = new Dictionary<int,string>();
            foreach (var vlan in vlans)
            {
                this.vlans.Add(vlan.vlanId, vlan.name);
            }
            this.profiles = new Dictionary<int,string>();
            foreach (var profile in profiles)
            {
                this.profiles.Add(profile.id, profile.name);
            }
        }
    }
}