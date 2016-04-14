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
            inventories[0] = new Inventory("storage", 1000);
            inventories[1] = new Inventory("money", 10000);
            inventories[2] = new Inventory("party", 1000);
            inventories[3] = new Inventory("promises", 10);
        }
    }
}
