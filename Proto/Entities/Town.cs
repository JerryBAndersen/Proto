using Proto.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class Town : Entity
    {
        public Town(string name, Vector2 position) {
            this.name = name;
            this.position = position;
            this.strength = 1000;

            inventories[0] = new Inventory("storage", 2000, this);
            inventories[1] = new Inventory("treasure", 10000, this);
            inventories[2] = new Inventory("party", 1000, this);
            inventories[3] = new Inventory("documents", 10, this);
        }
    }
}
