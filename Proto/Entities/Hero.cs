using Proto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class Hero : Entity, Destroyable
    {
        public Inventory inventory;
        Type priority;

        public Hero(Vector2 loc) {
            location = loc;
            inventory = new Inventory(128, id);
            name = id;
        }

        public void Destroy()
        {
            Console.WriteLine("Hero " + name + " died!");
            health = 0;
        }

        public override void Update()
        {
            if (health <= 0) {
                Destroy();
            }
            if (Program.gtime % Program.foodRate == 0) {
                inventory.UpdateConsumtion();
            }

            // this heros priority
            priority = typeof(Man);

            // if food is sufficient
            if (inventory.SCount(typeof(Man)) + 1 < inventory.SCount(typeof(Food)))
            {
                priority = typeof(Man);
            }
            else {
                priority = typeof(Food);
            }

            Console.WriteLine("Priority: " + priority.ToString());
            Console.WriteLine("Location: " + location.ToString());

            // empty villag(es) at this location
            foreach (Village v in Program.GetByLocation(location).OfType<Village>()) {
                if (v.inventory.SCount(priority) > 0)
                {
                    while (inventory.FreeSlots() < v.inventory.SCount(priority)) {
                        inventory.RemoveStorable(priority);
                    }
                    if (v.inventory.TransferAllOfType(priority, inventory))
                    {
                        Console.WriteLine("Transfered all from village " + v.id + " to Hero " + name);
                    }
                    else {
                        Console.WriteLine("Transfer to Hero failed!");
                    }
                    return;
                }
            }

            // look in other villages
            int ran = new Random().Next(Program.ents.OfType<Village>().Count());
            int cnt = 0;
            foreach (Village v in Program.ents.OfType<Village>()) {
                // if there is something in the village and there is no other hero on it
                if (v.inventory.SCount(priority) > 4 && Program.GetByLocation(v.location).OfType<Hero>().Count() < 1 && cnt - ran > 0) {
                    location = v.location;
                    Console.WriteLine("Changed Location to " + location);
                    return;
                } else if (Program.GetByLocation(v.location).OfType<Hero>().Count() > 0) {
                    // theres a hero on here
                    foreach (Hero h in Program.GetByLocation(v.location).OfType<Hero>()) {
                        if (h.inventory.SCount(typeof(Man)) > 
                            inventory.SCount(typeof(Man))) {

                            if (inventory.capacity >= inventory.SCount()+h.inventory.SCount(typeof(Food))) {

                            }
                            h.inventory.TransferAllOfType(typeof(Food), inventory);
                            h.inventory.RemoveStorable(typeof(Man), h.inventory.SCount(typeof(Man)));
                        }
                    }
                }
                cnt++;
            }
        }
    }
}
