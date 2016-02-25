using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    struct Vector2 {
        public float x;
        public float y;
        public Vector2(float x, float y) : this()
        {
            this.x = x;
            this.y = y;
        }

        override public string ToString() {
            return "(" + x + "," + y + ")";
        }

        public static Vector2 zero = new Vector2(0,0);
    }

    abstract class Entity
    {
        public int health;
        public string id;
        public string name;
        public Vector2 location;

        public Entity() {
            health = 100;
            id = Program.GetNewID();
            location = new Vector2(0f,0f);
        }
        
        abstract public void Update();
    }
}
