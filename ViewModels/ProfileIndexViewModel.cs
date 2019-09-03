using System.Collections.Generic;
using NetworkManager.Models;

namespace NetworkManager.Models
{
    public class ProfileIndexViewModel
    {
        public IEnumerable<NetworkManager.Models.Profile> Profiles { get; set; }
        public List<Vlan> Vlans { get; set; }

        public ProfileIndexViewModel(IEnumerable<NetworkManager.Models.Profile> Profiles, List<Vlan> Vlans)
        {
            this.Profiles = Profiles;
            this.Vlans = Vlans;
        }
    }
}