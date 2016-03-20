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
            inventory = new Inventory(200);
            money = new Inventory(10000);
            party = new Inventory(10);
            promises = new Inventory(10);
        }
    }
}
