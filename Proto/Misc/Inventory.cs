using Proto.Storables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc {
    class Inventory : List<Storable>{

        private string name;

        public int FreeCapacity {
            get {
                return this.Capacity - Count;
            }
        }

        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public Inventory(string name, int capacity) : base() {
            this.name = name;
            this.Capacity = capacity;
        }

        public bool TryAdd(Storable item) {
            if (FreeCapacity < 1) {
                return false;
            }
            return true;
        }

        public bool TryRemove(Storable item) {
            if (!this.Contains(item)) {
                return false;
            }
            return true;
        }

        public void Transfer(Storable storable, Inventory to) {            
            to.Add(storable);
            this.Remove(storable);
        }

        public bool TryTransfer(Storable storable, Inventory to) {
            if (!this.TryRemove(storable)) {
                return false;
            }
            if (!to.TryAdd(storable)) {
                return false;
            }
            return true;
        }

        public void TransferAll(Inventory to) {
            foreach (Storable s in this) {
                to.Add(s);
            }
            this.Clear();
        }

        public bool TryTransferAll(Inventory to) {
            foreach (Storable s in this) {
                if (!to.TryAdd(s)) {
                    return false;
                }
            }
            return true;
        }

        public void RemoveInventory(Inventory inv) {
            foreach (Storable s in inv) {
                this.Remove(s);
            }
        }

        public bool TryRemoveInventory(Inventory inv) {
            foreach (Storable s in inv) {
                if (!this.TryRemove(s)) {
                    return false;
                }
            }
            return true;
        }

        public void AddInventory(Inventory inv) {
            foreach (Storable s in inv) {
                this.Add(s);
            }
        }

        public bool TryAddInventory(Inventory inv) {
            if (inv.Count > this.FreeCapacity) {
                return false;
            }
            return true;
        }
    }
}
