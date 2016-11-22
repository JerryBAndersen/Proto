using Proto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc {
    class Storage : Inventory {
        public Storage(int capacity) : base("storage", capacity) {
            this.Capacity = capacity;
        }
        public Storage(int capacity, Entity owner) : base("storage", capacity, owner) {
            this.Capacity = capacity;
            this.owner = owner;
        }
    }
}
