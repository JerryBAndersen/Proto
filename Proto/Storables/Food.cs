using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Storables
{
    class Food : Storable, Perishable {
        int shelflife;
        public int Shelflife {
            get {
                return shelflife;
            }

            set {
                shelflife = value;
            }
        }

        public override Storable Clone() {
            return new Gold();
        }

        public bool Perish() {
            Shelflife--;
            return Shelflife < 0;
        }
    }
}
