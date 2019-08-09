using System.Collections.Generic;

namespace NetworkManager.Models
{
    public partial class SwitchModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Driver { get; set; }
        public int PortCount { get; set; }
    }
}