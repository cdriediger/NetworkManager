﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetworkManager.Models
{
    public partial class Ports
    {
        //static data
        public int id { get; set; }
        public int name { get; set; }

        //dymamic updated data
        public string description { get; set; }
        [Range(1, 4096)]
        public int vlan { get; set; }
        public virtual TaggedVlans taggedVlans { get; set; }
        public virtual Profile profile { get; set; }
        public string state { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lldpRemoteDeviceName {get; set;}
        public Boolean isUplink {get; set; }

        //relation data
        public int switchId { get; set; }
        public virtual Switches Switch { get; set; }

        public Ports()
        {
            state = "Up";
        }
    }
}
