using Proto.Entities;
using Proto.Misc;
using Proto.Storables;
using System;
using System.Collections.Generic;

namespace Proto
{
    class Program
    {
        static int gameTime = 0;
        static int lastId = 0;
        static List<Entity> entities;
        static Hero player;
        static Town[] towns;
        static Random ran = new Random();

        static void Main(string[] args)
        {
            InitializeEntities();

            // Update Loop
            while (true) {
                UpdateLogic();
                gameTime++;
            }
        }

        private static void UpdateLogic()
        {
            Console.WriteLine("Tick.");

            UpdateEntities();

            string input = "";
            do
            {
                Console.WriteLine("Input:");
                input = Console.ReadLine();
                try
                {
                    Input(input);
                }
                catch (InvalidInputException ie)
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (!string.IsNullOrEmpty(input));
        }

        private static void UpdateEntities()
        {

        }

        private static void InitializeEntities()
        {
            entities = new List<Entity>();

            player = new Hero("Lord Hosenschlitz", 50, new Vector2(0, 1));
            entities.Add(player);

            towns = new Town[10];
            for(int i = 0; i < towns.Length; i++) {
                towns[i] = new Town(RandomName() + " Town", new Vector2(ran.Next(-10, 10), ran.Next(-10, 10)));
            }
        }

        public static void Kill(Entity e) {
            int index = entities.IndexOf(e);
            if (index > -1) {
                if (e.GetType() == typeof(Hero)) {
                    entities.Insert(index, new DeadHero(e as Hero));
                    entities.RemoveAt(index + 1);
                } else if (e.GetType() == typeof(Town)) {
                    entities.Insert(index, new DeadTown(e as Town));
                    entities.RemoveAt(index + 1);
                }
            }
        }

        public static int CreateId()
        {
            lastId++;
            return lastId;
        }

        public static void Input(string input) {
            char[] seperators = { ' ', '\t'};
            string[] args = input.Split(seperators);
            if (args.Length < 1) {
                // if input is empty
                return;
            }
            string cmd = args[0];

            // MOVE
            if (cmd == "move")
            {
                bool validx = false, validy = false;
                int x = 0, y = 0;
                if (Int32.TryParse(args[1], out x))
                {
                    validx = true;
                }
                if (Int32.TryParse(args[2], out y))
                {
                    validy = true;
                }
                if (validx && validy)
                {
                    player.Move(new Vector2(x, y));
                }
                else {
                    Console.WriteLine("No valid move");
                }
            }
        }

        public static string RandomName() {
            int length = ran.Next(2,6);
            string s = "";
            while(length > 0)
            {
                int sillable = ran.Next(0,4);
                switch (sillable)
                {
                    case 0:
                        {
                            s += "be";
                            break;
                        }
                    case 1:
                        {
                            s += "ne";
                            break;
                        }
                    case 2:
                        {
                            s += "ka";
                            break;
                        }
                    case 3:
                        {
                            s += "no";
                            break;
                        }
                    case 4:
                        {
                            s += "na";
                            break;
                        }
                }
                length--;
            }
            char c = s[0];
            s = (char)(c - 32) + s.Substring(1);
            return s;
        }
    }        
}
