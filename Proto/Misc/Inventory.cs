using Proto.Entities;
using Proto.Storables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc {
    class Inventory : List<Storable>{

        protected string name;
        protected Entity owner;

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

        internal Entity Owner {
            get {
                return owner;
            }

            set {
                owner = value;
            }
        }
                
        public Inventory(string name, int capacity) : base() {
            this.name = name;

            this.Capacity = capacity;
        }
        public Inventory(string name, int capacity, Entity owner) : base() {
            this.name = name;
            this.Capacity = capacity;
            this.owner = owner;
        }

        public bool TryAdd(Storable item) {
            if (FreeCapacity < 1) {
                return false;
            }
            return true;
        }
        public bool TryAdd(Storable item, int count) {
            if (FreeCapacity < Count) {
                return false;
            }
            return true;
        }
        public bool TryAdd<T>(int count) {
            if (FreeCapacity < Count) {
                return false;
            }
            return true;
        }
        public bool TryAdd(Inventory inv) {
            if (inv.Count > this.FreeCapacity) {
                return false;
            }
            return true;
        }
        //public void Add(Storable item, int count) {
        //    for (int i = 0; i < count; i++) {
        //        Storable clone = item.Clone();
        //        Add(clone);
        //    }
        //}
        public void Add(Inventory inv) {
            foreach (Storable s in inv) {
                this.Add(s);
            }
        }
        public void Add<T>(int count) {
            Storable.SType t = Storable.DetermineType(typeof(T));
            for (int i = 0; i < count; i++) {
                switch (t) {
                    case Storable.SType.Food: {
                            Add(new Food());
                            break;
                        }
                    case Storable.SType.Gold: {
                            Add(new Gold());
                            break;
                        }
                    case Storable.SType.Man: {
                            Add(new Man());
                            break;
                        }
                    case Storable.SType.Promise: {
                            Add(new Promise());
                            break;
                        }
                }
            }
        }


        public bool TryRemove(Storable item) {
            if (!this.Contains(item)) {
                return false;
            }
            return true;
        }
        public bool TryRemove(Inventory inv) {
            foreach (Storable s in inv) {
                if (!this.TryRemove(s)) {
                    return false;
                }
            }
            return true;
        }
        public void Remove(Inventory inv) {
            for (int i = inv.Count - 1; i > 0; i--) {
                this.Remove(inv[i]);
            }
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
        public void Transfer(Storable storable, Inventory to) {            
            to.Add(storable);
            this.Remove(storable);
        }

        public bool TryTransferAll(Inventory to) {
            ForEach(stor => TryTransfer(stor,to));
            if (to.FreeCapacity < Count) {
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

        public void PrintContents() {
            foreach (Storable s in this) {
                Console.WriteLine(s.GetType());
            }
        }

        public int CountStorables<T>() {
            int count = 0;
            foreach (Storable s in this) {
                if (s is T) {
                    count++;
                }
            }
            return count;
        }
    }
}
