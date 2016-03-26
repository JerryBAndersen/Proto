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

        public static Vector2 zero = new Vector2(0, 0);
    }

    class Entity
    {
        public string name;
        public int id;
        public int health;
        public int strength;
        public Vector2 position;
        public Inventory inventory, money, party, promises;

        public Entity() {
            id = Program.GetId();
            name = id.ToString();
            health = 100;
            strength = 0;
            position = Vector2.zero;
            inventory = new Inventory(0);
            money = new Inventory(0);
            party = new Inventory(0);
            promises = new Inventory(0);
        }

        public void Attack(Entity target) {
            target.health = target.health - Math.Max(0, this.strength - target.strength);
            if (target.health < 1) {
                Program.Kill(target);
            }
        }

        public bool Trade(Entity partner, Offer o) {
            if (partner.TestOffer(o)) {
                o.Apply();
                return true;
            } else {
                return false;
            }
        }

        public bool TestOffer(Offer offer) {
            if (offer.you.strength > (int) (offer.me.strength*1.5f)) {
                offer.accepted = true;
                return true;
            } else {
                return false;
            }
        }
    }
}
