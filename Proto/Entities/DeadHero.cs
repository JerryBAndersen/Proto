using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class DeadHero : Entity {
        public Hero h;

        public DeadHero(Hero h) {
            this.h = h;
            this.name = h.name + "'s corpse";
            this.health = 0;
            h.health = 0;
        }
    }
}
