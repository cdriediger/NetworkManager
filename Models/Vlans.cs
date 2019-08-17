namespace NetworkManager.Models
{
    public partial class Vlan
    {
        public int id { get; set; }
        public int vlanId { get; set; }
        public string name { get; set; }

        //relation data
        public int portId { get; set; }
        public virtual Ports port { get; set; }

        public Vlan(int vlanId, string name)
        {
            this.vlanId = vlanId;
            this.name = name;
        }

        public Vlan(int vlanId)
        {
            this.vlanId = vlanId;
        }
    }
}