using Proto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class Village : Entity, Destroyable
    {
        public Inventory inventory;

        public Village(Vector2 loc) {
            inventory = new Inventory(32, id);
            name = id;
            location = loc;
            Console.WriteLine("Created village " + name + " at " + location);
        }

        public override void Update()
        {
            if (Program.gtime % (7 + (int.Parse(id) % 2)) == 0)
            {
                if (inventory.SCount(typeof(Food)) < 9) {
                    inventory.AddStorable(4, typeof(Food));
                }
                if (inventory.SCount(typeof(Man)) < 5) {
                    inventory.AddStorable(1, typeof(Man));
                }
            }

        }
        
        public void Destroy()
        {
            Console.WriteLine("Village " + id + " was destroyed!");
            health = 0;
        }
        
    }
}
