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
        public Hero() {
            strength = 1;
            inventories[0] = new Inventory("storage", 10);
            inventories[1] = new Inventory("money", 10000);
            inventories[2] = new Inventory("party", 1000);
            inventories[3] = new Inventory("promises", 10);
        }

        public Hero(string name, int strength, Vector2 position)
        {
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
