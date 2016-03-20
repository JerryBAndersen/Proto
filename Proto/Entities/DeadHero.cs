using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class DeadHero : Entity {
        private Hero h;

        public DeadHero(Hero h) {
            this.h = h;
        }
    }
}
