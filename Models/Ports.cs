using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetworkManager.Models
{
    public partial class Ports
    {
        public int id { get; set; }
        public int name { get; set; }
        public string description { get; set; }
        [Range(1, 4096)]
        public int vlan { get; set; }

        public int switchId { get; set; }
        public virtual Switches Switch { get; set; }

        [NotMapped]
        public string state { get; set; }

        public Ports()
        {
            state = "Up";
        }
    }

    
}
