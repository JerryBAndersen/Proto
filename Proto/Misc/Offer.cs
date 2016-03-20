using Proto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc
{
    class Offer
    {
        public Entity me, you;
        public Inventory[] mine, yours;
        public bool accepted = false;

        public Offer(Entity me, Entity you, Inventory[] mine, Inventory[] yours) {
            this.me = me;
            this.you = you;
            this.mine = mine;
            this.yours = yours;
        }

        public void Apply() {
            if (accepted) {

            }
        }

    }
}
