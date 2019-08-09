using System.Collections.Generic;
using NetworkManager.Models;

namespace NetworkManager.Models
{
    public class DetailsViewModel
    {
        public Switches sw {get; set;}
        public Dictionary<int, string> vlans {get; set;}

        public DetailsViewModel(Switches sw, Dictionary<int,string> vlans)
        {
            this.sw = sw;
            this.vlans = vlans;
        }
    }
}