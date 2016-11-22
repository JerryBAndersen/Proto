using Proto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc {
    class Party : Inventory{
        public Party(int capacity) : base("documents", capacity) {
            this.Capacity = capacity;
        }
        public Party(int capacity, Entity owner) : base("documents", capacity, owner) {
            this.Capacity = capacity;
            this.owner = owner;
        }
    }
}
