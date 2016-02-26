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
            for (int x = 0; x < 20; x++) {
                for (int y = 0; y < 20; y++) {
                    ents.Add(new Village(new Vector2(x, y)));
                }
            }
            for (int i = 0; i < 20; i++) {
                ents.Add(new Hero(new Vector2(i, -1f)));
            }

            while (true)
            {
                Console.WriteLine("\nTick.");
                // handle movement and gathering of men/food
                foreach (Entity en in ents)
                {
                    en.Update();
                }
                // handle hero collisions and robbing
                foreach (Entity en in ents)
                {                    
                    if (en.GetType() == typeof(Village))
                    {
                        Hero winner = null;
                        foreach (Hero h in GetByLocation(en.location, typeof(Hero))) {
                            if (winner == null) {
                                winner = h;
                            }
                            if (winner.party.SCount() < h.party.SCount()) {
                                winner.inventory.TransferAll(h.inventory);
                                winner.party.RemoveStorable(typeof(Man), winner.party.SCount(typeof(Man)));
                                winner.location = GetRandomLocation();
                                Console.WriteLine("Hero " + h.name + " robbed hero " + winner.name);
                                winner = h;

                            }
                        }

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

        public static Entity[] GetByLocation(Vector2 loc, Type t) {
            List<Entity> entsAtLoc = new List<Entity>();
            foreach (Entity en in ents) {
                if (en.GetType() == t & en.location.Equals(loc)) {
                    entsAtLoc.Add(en);
                }
            }
            return entsAtLoc.ToArray();
        }

        public static Entity[] GetByType(Type t) {
            List<Entity> entsAtLoc = new List<Entity>();
            foreach (Entity en in ents) {
                if (en.GetType() == t) {
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
