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
        public Inventory inventory, party;
        Type priority;

        public Hero(Vector2 loc) {
            location = loc;
            inventory = new Inventory(128, id);
            party = new Inventory(128, id);
            name = id;
        }

        public void Destroy()
        {
            Console.WriteLine("Hero " + name + " died!");
            health = 0;
        }

        public override void Update()
        {
            Console.WriteLine("Hero " + name + " has:");
            Console.WriteLine(inventory.GetReport());
            Console.WriteLine(party.GetReport());

            if (health <= 0) {
                Destroy();
            }
            if (Program.gtime % Program.foodRate == 0) {
                party.UpdateConsumtion(inventory);
            }

            // this heros priority
            priority = typeof(Man);

            // if food is sufficient
            if (party.SCount() + 1 < inventory.SCount(typeof(Food)))
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
                // is there a hero on here
                if (Program.GetByLocation(v.location).OfType<Hero>().Count() > 0 ) {
                    // theres a hero on here
                    foreach (Hero h in Program.GetByLocation(v.location).OfType<Hero>()) {
                        if (h.id != this.id) {
                            if (h.inventory.SCount(typeof(Man)) >
                            inventory.SCount(typeof(Man))) {
                                // kill his men
                                h.party.RemoveStorable(typeof(Man), h.party.SCount(typeof(Man)));
                                // steal his food
                                int fdcnt = h.inventory.SCount(typeof(Food));
                                h.inventory.RemoveStorable(typeof(Food), h.inventory.SCount(typeof(Food)));
                                // banish him
                                h.location = Program.GetRandomLocation();
                                // add only as much as hero can carry
                                inventory.AddStorable(Math.Min(inventory.FreeSlots(), fdcnt), typeof(Food));
                                return;
                            }
                            else {
                                break;
                            }
                        }
                    }
                }

                if (v.inventory.SCount(priority) > 0 || v.party.SCount(priority) > 0)
                {
                    if (v.inventory.TransferAllOfType(priority, inventory) &&
                        v.party.TransferAllOfType(priority, party))
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
                if (Program.GetByLocation(v.location).OfType<Hero>().Count() < 1 && cnt - ran > 0) {
                    if (v.inventory.SCount(priority) > 4 || v.party.SCount(priority) > 2) {
                        location = v.location;
                        Console.WriteLine("Changed Location to " + location);
                        return;
                    }
                } else if (Program.GetByLocation(v.location).OfType<Hero>().Count() > 0) {
                    // theres a hero on there
                    foreach (Hero h in Program.GetByLocation(v.location).OfType<Hero>()) {
                        if (h.party.SCount(typeof(Man)) > 
                            party.SCount(typeof(Man))) {
                            // and he's weaker than this hero
                            location = v.location;
                            Console.WriteLine("Changed Location to " + location);
                            return;
                        }
                    }
                }
                cnt++;
            }
        }
    }
}
