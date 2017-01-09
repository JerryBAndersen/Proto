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

    struct Relation {
        private int value;
        public Entity entity;

        public int Value {
            get {
                return value;
            }

            set {
                // cap values to -100 to 100
                this.value = Math.Min(100,Math.Max(-100,value));
            }
        }

        public Relation(int value, Entity entity) {
            this.value = value;
            this.entity = entity;
        }
    }
    class Entity {
        public string name;
        public int id;
        public int health;
        public int strength;
        public Vector2 position;
        /// <summary>
        /// Inventories contain storage [0], treasure [1], party [2], documents [3], ...
        /// </summary>
        public Inventory[] inventories = new Inventory[4];
        public List<Offer> offers;

        private List<Relation> relations;

        public Entity() {
            id = Program.CreateId();
            name = id.ToString();
            health = 100;
            strength = 0;
            position = Vector2.zero;
            inventories.Initialize();
            offers = new List<Offer>();

            relations = new List<Relation>();            
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
        
        public bool IsKnown(Entity other) {
            foreach (Relation r in relations) {
                if (r.entity == other) {
                    return true;
                }
            }
            return false;
        }

        public int RelationTowards(Entity other) {
            foreach (Relation r in relations) {
                if (r.entity == other) {
                    return r.Value;
                }
            }
            throw new Exception();
        }

        public void Meet(Entity other) {
            // Do I know you?
            if (!IsKnown(other)) {
                relations.Add(new Relation(0, other));
            }
            int rel = RelationTowards(other);
            // Can I trust you?
            if (rel > 0) {
                // How much can I trust you?
                float relationfactor = 100f / rel;
                // Who do you know and how well?
                foreach (Relation r in other.relations) {
                    // Do I know that person?
                    if (!IsKnown(r.entity)) {
                        // I can only trust your opinion
                        // as much as I can trust you.
                        relations.Add(new Relation((int)Math.Round(r.Value * relationfactor), r.entity));
                    } else {
                        // I know this one, what is your opinion?
                        Relation f = relations.Find(p => { return p.entity==r.entity; });
                        f.Value = (int)Math.Round(f.Value * (1f - relationfactor) + relationfactor * r.Value);
                    }
                }
            }
        }
    }
}
