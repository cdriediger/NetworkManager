namespace NetworkManager.Models
{
    public partial class Profile
    {
        public int id { get; set; }
        public string name { get; set; }

        public int nativeVlan { get; set; }
        public virtual TaggedVlans taggedVlans { get; set; }
    }
}