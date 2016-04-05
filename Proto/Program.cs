using Proto.Entities;
using Proto.Misc;
using Proto.Storables;
using System;
using System.Collections.Generic;

namespace Proto
{
    class Program
    {
        static int gtime = 0;
        static int lastId = 0;
        static List<Entity> entities;
        static Hero player;
        static Random ran = new Random();

        static void Main(string[] args)
        {
            InitializeEntities();

            // Update Loop
            while (true) {
                UpdateLogic();
            }
        }

        private static void InitializeEntities() {
            entities = new List<Entity>();

            //Hero[] heroes = new Hero[6];
            //for (int i = 0; i < heroes.Length; i++) {
            //    heroes[i] = new Hero("Lord " + NumberToWords(lastId + 1), 3, new Vector2());
            //}
            //entities.AddRange(heroes);
            
            player = new Hero("Lord Hosenschlitz", 3, new Vector2(0, 1));
            player.inventories[0].Add(new Food());
            entities.Add(player);


            Hero npc = new Hero("Lord Hasenfuß", 3, new Vector2(0, 1));
            npc.inventories[0].Add(new Food());
            npc.inventories[0].Add(new Food());
            entities.Add(npc);

            Town[] towns = new Town[64];
            for (int i = 0; i < towns.Length; i++) {
                double rand = ran.NextDouble() - .5d;
                towns[i] = new Town("St. " + NumberToWords(lastId + 1), new Vector2((int)(100d * rand), GetId()));
            }
            entities.AddRange(towns);
        }

        private static void UpdateLogic() {
            Console.WriteLine("Tick.");

            // Print Locations
            #region printLocations
            foreach (Hero h in entities.FindAll(h => typeof(Hero) == h.GetType())) {
                Console.WriteLine(h.name + " is at " + h.position.ToString());
                Console.WriteLine("And has " + h.inventories[0].Count + " food.");
            }
            foreach (Town t in entities.FindAll(t => typeof(Town) == t.GetType())) {
                if (gtime == 0) {
                    Console.WriteLine(t.name + " is at " + t.position.ToString());
                    //Console.WriteLine("And has ID " + t.id);
                }
            } 
            #endregion

            // Input handling
            string input = "";
            do {
                Console.WriteLine("Input:");
                input = Console.ReadLine();
                PlayerInput(input);
            } while (!string.IsNullOrEmpty(input));
            gtime++;
        }                

        public static void PlayerInput(string input) {
            // MOVE
            if (input.Contains("move")) {
                bool validx = false, validy = false;
                int x = 0, y = 0;
                foreach (string s in input.Split(' ')) {
                    if (!validx) {
                        if (Int32.TryParse(s, out x)) {
                            validx = true;
                        }
                    } else {
                        if (Int32.TryParse(s, out y)) {
                            validy = true;
                        }
                    }

                }
                if (validx && validy) {
                    player.Move(new Vector2(x, y));
                } else {
                    Console.WriteLine("No valid move");
                }
            }
            // ATTACK
            else if (input.Contains("attack")) {
                Console.WriteLine("Todo: Attack");
            }
            // ADD FOOD
            else if (input.Contains("playeraddfood")) {
                Console.WriteLine("Add Food to Player.");
                if (player.inventories[0].TryAdd(new Food())) {
                    player.inventories[0].Add(new Food());
                } else {
                    Console.WriteLine("Can't add Food to Player.");
                }

            }
            // SEND OFFER
            else if (input.Contains("createoffer")) {
                Console.WriteLine("Create Offer.");
                Entity you = ChooseEntity();
                Offer offer = new Offer(player, you, ChooseItemsFromInventory(player), ChooseItemsFromInventory(you));
                you.offers.Add(offer);
            } 
            // ADD FOOD
            else if (input.Contains("addfood")) {
                player.inventories[0].Add(new Food());
            }
            // ACCEPT OFFER
            else if (input.Contains("acceptoffer")) {
                Console.WriteLine("Accept Offer.");
                for (int i = 0; i < player.offers.Count; i++) {
                    Console.WriteLine("Offer " + i + ": " + player.offers[i].GetHashCode());
                }
                Console.WriteLine("Enter offer hash to accept:");
                string offerHash = Console.ReadLine();
                foreach (Offer o in player.offers) {
                    if (o.GetHashCode().ToString() == offerHash) {
                        if (o.TryApply()) {
                            o.Apply();
                        }
                    }
                }                
            }
            // REJECT OFFER
            else if (input.Contains("rejectoffer")) {
                Console.WriteLine("Reject Offer.");
                for (int i = 0; i < player.offers.Count; i++) {
                    Console.WriteLine("Offer " + i + ": " + player.offers[i].GetHashCode());
                }
                Console.WriteLine("Enter offer hash to reject:");
                string offerHash = Console.ReadLine();
                foreach (Offer o in player.offers) {
                    if (o.GetHashCode().ToString() == offerHash) {
                        o.Reject();
                    }
                }
            }
        }

        public static Inventory[] ChooseItemsFromInventory(Entity source) {
            List<Inventory> invlist = new List<Inventory>();
            // choose which one to select from player
            foreach (Inventory inv in source.inventories) {
                Inventory clone = new Inventory(inv.Name, inv.Capacity);
                // get all hashcodes
                List<string> hashcodes = inv.ConvertAll<string>(p => p.GetHashCode().ToString());
                Console.WriteLine("Inventory " + inv.Name + " contents:");
                for (int i = 0; i < inv.Count; i++) {
                    Console.WriteLine("item " + i + ": " + hashcodes[i]);
                }
                Console.WriteLine("Enter item hashes seperated by spaces:");
                string selection = Console.ReadLine();
                if (!string.IsNullOrEmpty(selection)) {
                    string[] hashes = selection.Split(' ');
                    foreach (string hash in hashes) {
                        for (int j = 0; j < hashcodes.Count; j++) {
                            if (hash == hashcodes[j]) {
                                clone.Add(inv[j]);
                            }
                        }
                    }
                }                
                invlist.Add(clone);
            }           
            return invlist.ToArray();
        }

        public static Entity ChooseEntity() {
            // choose partner
            PrintEntities(entities);
            Console.WriteLine("Who do you want to trade with?");
            int id = 0;
            string partner = Console.ReadLine();
            if (Int32.TryParse(partner, out id) || entities[id] != null) {
                return entities[id];
            } else {
                throw new InvalidInputException(); 
            }
        }

        public static void PrintEntities(List<Entity> es) {
            for (int i = 0; i < es.Count; i++) {
                Console.WriteLine("entities[" + i + "]: name:" + es[i].name);
            }
        }

        public static int GetId() {
            return lastId++;
        }

        public static void Kill(Entity e) {
            if (e.GetType() == typeof(Hero)) {
                entities[entities.IndexOf(e)] = new DeadHero((Hero)e);
            } else if (e.GetType() == typeof(Town)) {
                entities[entities.IndexOf(e)] = new DeadTown((Town)e);
            }
            
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }        
}
