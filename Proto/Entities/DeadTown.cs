using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities {
    class DeadTown : Entity {
        private Town t;

        public DeadTown(Town t) {
            this.t = t;
        }
    }
}
