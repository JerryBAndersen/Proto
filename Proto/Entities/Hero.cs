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
            this.strength = 1;
            this.inventory = new Inventory(10);
            money = new Inventory(10000);
            party = new Inventory(1000);
            promises = new Inventory(10);
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
