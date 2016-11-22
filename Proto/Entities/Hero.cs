using Proto.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class Hero : Entity
    {
        public Hero(string name, int strength, Vector2 position)
        {
            inventories[0] = new Inventory("storage", 100, this);
            inventories[1] = new Inventory("treasure", 10000, this);
            inventories[2] = new Inventory("party", 1000, this);
            inventories[3] = new Inventory("documents", 10, this);
            this.name = name;
            this.strength = strength;
            this.position = position;
        }

        public void Move(Vector2 destination)
        {
            position = destination;
        }

        public void CreatePromise() {

        }
    }
}
