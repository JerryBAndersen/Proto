using Proto.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Storables
{
    class Man : Storable, Feedable {
        int nutrition;
        int maxnutrition = 3;
        public int Nutrition {
            get {
                return nutrition;
            }

            set {
                nutrition = Math.Min(Maxnutrition,value);
            }
        }

        public int Maxnutrition {
            get {
                return maxnutrition;
            }

            set {
                maxnutrition = value;
            }
        }

        public void Feed() {
            Nutrition++;
        }

        public bool Starve() {
            Nutrition--;
            return Nutrition < 1;
        }


        public override Storable Clone() {
            Man c = new Man();
            c.Nutrition = this.nutrition;
            return c;
        }
    }
}
