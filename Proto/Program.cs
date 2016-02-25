using Proto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto
{
    class Program
    {
        public static int gtime = 0;
        public static List<Entity> ents;
        public static int foodRate = 7;
        static int id;
        static void Main(string[] args)
        {
            Console.WriteLine("Proto!");
            ents = new List<Entity>();
            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 4; y++) {
                    ents.Add(new Village(new Vector2(x, y)));
                }
            }
            for (int i = 0; i < 10; i++) {
                ents.Add(new Hero(new Vector2(i, -1f)));
            }

            while (true)
            {
                Console.WriteLine("\nTick.");
                foreach (Entity en in ents)
                {
                    en.Update();
                }

                foreach (Entity en in ents)
                {
                    if (en.GetType() == typeof(Village))
                    {
                        //Console.WriteLine("Village " + ((Village)en).name + " has:");
                        //Console.WriteLine(((Village)en).inventory.GetReport());
                        //Console.WriteLine(((Village)en).party.GetReport());
                    }
                }

                ConsoleKeyInfo c = Console.ReadKey();
                if (c.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
                gtime++;
            }
        }

        public static string GetNewID()
        {
            return "" + id++;
        }

        public static Entity GetByID(string id)
        {
            foreach (Entity en in ents)
            {
                if (en.id == id)
                {
                    return en;
                }
            }
            return null;
        }

        public static Entity[] GetByLocation(Vector2 loc)
        {
            List<Entity> entsAtLoc = new List<Entity>();
            foreach (Entity en in ents)
            {
                if (en.location.Equals(loc))
                {
                    entsAtLoc.Add(en);
                }
            }
            return entsAtLoc.ToArray();
        }

        public static Vector2 GetRandomLocation() {
            int i = (int) (ents.OfType<Village>().Count() * new Random().NextDouble());
            foreach (Village v in ents.OfType<Village>()) {
                if (i > 0) {
                    i--;
                }
                else {
                    return v.location;
                }
            }
            return Vector2.zero;
        }
    }
}
