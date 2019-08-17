namespace NetworkManager.Models
{
    public partial class TaggedVlan
    {
        public int id { get; set; }
        public int vlanId { get; set; }

        //relation data
        public int portId { get; set; }
        public virtual Ports port { get; set; }

        public TaggedVlan(int vlanId)
        {
            this.vlanId = vlanId;
        }
    }
}