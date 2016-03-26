using Proto.Storables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc {
    class Inventory : List<Storable>{

        public Inventory(int capacity) {
            this.Capacity = capacity;
        }

        public void Transfer(Storable storable, Inventory to) {
            to.Add(storable);
            this.Remove(storable);
        }

        public void TransferAll(Inventory to) {
            foreach (Storable s in this) {
                to.Add(s);
                this.Remove(s);
            }
        }
    }
}
