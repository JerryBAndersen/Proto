using Proto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc {
    class Documents : Inventory{
        public Documents(int capacity) : base("documents", capacity) {
            base.name = name;
            this.Capacity = capacity;
        }
        public Documents(int capacity, Entity owner) : base("documents", capacity, owner) {
            this.Capacity = capacity;
            this.owner = owner;
        }
    }
}
