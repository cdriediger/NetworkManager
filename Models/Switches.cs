using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetworkManager.Adapters;


namespace NetworkManager.Models
{
    public partial class Switches
    {
        public int id { get; set; }
        public string ipv4 { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int model { get; set; }

        public virtual ICollection<Ports> ports { get; set; }

        [NotMapped]
        public string location { get; set; }
        [NotMapped]
        public string name { get; set; }
        [NotMapped]
        public string modelName {get; set;}
        [NotMapped]
        public HPE_Aruba_Adapter adapter {get; set;}

        public Switches()
        {
            ports = new List<Ports>();
        }
    }
}
