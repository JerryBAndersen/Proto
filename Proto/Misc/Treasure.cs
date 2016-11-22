using Proto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc {
    class Treasure : Inventory {
        public Treasure(int capacity) : base("treasure", capacity) {
            this.Capacity = capacity;
        }
        public Treasure(int capacity, Entity owner) : base("treasure", capacity, owner) {
            this.Capacity = capacity;
            this.owner = owner;
        }
    }
}
