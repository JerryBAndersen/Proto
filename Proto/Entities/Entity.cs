using Proto.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    struct Vector2
    {
        public float x;
        public float y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        override public string ToString()
        {
            return "(" + x + "," + y + ")";
        }

        public bool Equals(Vector2 other) {
            return this.x == other.x && this.y == other.y;
        }

        public static Vector2 zero = new Vector2(0, 0);
    }

    class Entity {
        public string name;
        public int id;
        public int health;
        public int strength;
        public Vector2 position;
        /// <summary>
        /// Inventories contain storage [0], money [1], party [2], promises [3], ...
        /// </summary>
        public Inventory[] inventories = new Inventory[4];
        public List<Offer> offers;

        public Entity() {
            id = Program.GetId();
            name = id.ToString();
            health = 100;
            strength = 0;
            position = Vector2.zero;
            inventories.Initialize();
            offers = new List<Offer>();
        }

        public void Attack(Entity target) {
            if (this.strength == target.strength) {
                if (DateTime.Now.Second % 10 < 5) {
                    target.health = target.health - this.strength;
                    if (target.health < 1) {
                        Program.Kill(target);
                    }
                    this.health = this.health - target.strength;
                    if (this.health < 1) {
                        Program.Kill(this);
                        Console.WriteLine("kill ref to self");
                    }
                } else {
                    this.health = this.health - target.strength;
                    if (this.health < 1) {
                        Program.Kill(this);
                        Console.WriteLine("kill ref to self");
                    }
                    target.health = target.health - this.strength;
                    if (target.health < 1) {
                        Program.Kill(target);
                    }
                }
            } else {
                this.health = this.health - target.strength;
                if (this.health < 1) {
                    Program.Kill(this);
                    Console.WriteLine("kill ref to self");
                    return;
                }
                target.health = target.health - this.strength;
                if (target.health < 1) {
                    Program.Kill(target);
                    return;
                }
            }
        }
    }
}
