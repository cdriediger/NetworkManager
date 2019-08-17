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

    public class TaggedVlans : ICollection<Vlan>
    {   

        ICollection<Vlan> _items;


        public TaggedVlans() {
            // Default to using a List<T>.
            _items = new List<Vlan>();
        }

        protected TaggedVlans(ICollection<Vlan> collection) {
            // Let derived classes specify the exact type of ICollection<T> to wrap.
            _items = collection;
        }

        public void Add(Vlan item) { 
            _items.Add(item); 
        }

        public void Clear() { 
            _items.Clear(); 
        }

        public bool Contains(Vlan item) { 
            foreach (Vlan vlan in _items)
            {
                if (vlan.vlanId == item.vlanId)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Vlan[] array, int arrayIndex) { 
            _items.CopyTo(array, arrayIndex); 
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Vlan item)
        {
            return _items.Remove(item);
        }

        public IEnumerator<Vlan> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
