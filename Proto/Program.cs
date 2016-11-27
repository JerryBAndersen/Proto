using Proto.Entities;
using Proto.Misc;
using Proto.Storables;
using System;
using System.Collections.Generic;

namespace Proto
{
    class Program
    {
        //static int gtime = 0;
        //static int lastId = 0;
        //static List<Entity> entities;
        //static Hero player;
        //static Town[] towns;
        //static Random ran = new Random();

        static void Main(string[] args)
        {
            InitializeEntities();

            // Update Loop
            while (true) {
                UpdateLogic();
            }
        }

        private static void UpdateLogic()
        {
            Console.WriteLine("Tick.");
            #region inputhandling
            string input = "";
            do
            {
                Console.WriteLine("Input:");
                input = Console.ReadLine();
                try
                {
                    PlayerInput(input);
                }
                catch (InvalidInputException ie)
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (!string.IsNullOrEmpty(input));
            #endregion
        }

        private static void PlayerInput(string input)
        {
            
        }

        private static void InitializeEntities()
        {
            
        }

        internal static void Kill(Entity target)
        {
            throw new NotImplementedException();
        }

        internal static int GetId()
        {
            throw new NotImplementedException();
        }

        //private static void InitializeEntities() {
        //    entities = new List<Entity>();

        //    //Hero[] heroes = new Hero[6];
        //    //for (int i = 0; i < heroes.Length; i++) {
        //    //    heroes[i] = new Hero("Lord " + NumberToWords(lastId + 1), 3, new Vector2());
        //    //}
        //    //entities.AddRange(heroes);

        //    player = new Hero("Lord Hosenschlitz", 50, new Vector2(0, 1));
        //    player.inventories[0].Add(new Food());
        //    entities.Add(player);


        //    Hero npc = new Hero("Lord Hasenfuß", 51, new Vector2(0, 1));
        //    npc.inventories[0].Add(new Food());
        //    npc.inventories[0].Add(new Food());
        //    entities.Add(npc);

        //    towns = new Town[10];
        //    for (int i = 0; i < towns.Length; i++) {
        //        double rand = ran.NextDouble() - .5d;
        //        Vector2 pos = new Vector2((int)(100d * rand), GetId());
        //        towns[i] = new Town("St. " + NumberToWords(lastId + 1), pos);
        //    }
        //    entities.AddRange(towns);
        //}

        //private static void UpdateLogic() {
        //    Console.WriteLine("Tick.");

        //    RudimentaryAI();

        //    // Print locations
        //    #region printLocations
        //    //foreach (Hero h in entities.FindAll(h => typeof(Hero) == h.GetType())) {
        //    //    Console.WriteLine(h.name + " is at " + h.position.ToString());
        //    //    Console.WriteLine("And has " + h.inventories[0].CountStorables<Food>() + " food.");
        //    //}
        //    //foreach (Town t in entities.FindAll(t => typeof(Town) == t.GetType())) {
        //    //    if (true) {
        //    //        Console.WriteLine(t.name + " is at " + t.position.ToString());
        //    //        Console.WriteLine("And has " + t.inventories[0].CountStorables<Food>() + " food.");
        //    //        Console.WriteLine("And has " + t.inventories[2].CountStorables<Man>() + " men.");
        //    //    }
        //    //}
        //    #endregion

        //    // Input handling
        //    #region inputhandling
        //    string input = "";
        //    do {
        //        Console.WriteLine("Input:");
        //        input = Console.ReadLine();
        //        if (entities.IndexOf(player) > -1) {
        //            try {
        //                PlayerInput(input);
        //            } catch (InvalidInputException ie) {
        //                Console.WriteLine("Invalid input.");
        //            }
        //        } else {
        //            Console.WriteLine("Player isn't alive anymore.");
        //        }
        //    } while (!string.IsNullOrEmpty(input)); 
        //    #endregion

        //    gtime++;
        //}

        //enum EntityType{
        //    Hero, Town
        //}

        //private static EntityType TypeEnum(Entity e) {

        //    EntityType et = EntityType.Hero;
        //    if (e is Town)
        //        et = EntityType.Town;
        //    return et;
        //}

        //private static void RudimentaryAI() {
        //    foreach (Entity e in entities) {
        //        int population = e.inventories[2].CountStorables<Man>();
        //        EntityType et = TypeEnum(e);
        //        switch (et) {
        //            case EntityType.Hero: {
        //                    int totaltaxes = 10;
        //                    if (e.inventories[1].TryAdd<Gold>(totaltaxes)) {
        //                        e.inventories[1].Add<Gold>(totaltaxes);
        //                    }

        //                    if (e != player) {
        //                        // move randomly
        //                        if (ran.Next(10) > 6) {
        //                            // to a random town
        //                            Town t = towns[ran.Next(towns.Length - 1)];
        //                            (e as Hero).Move(t.position);
        //                            e.Meet(t);
        //                            t.Meet(e);

        //                        }

        //                        // Accept all offers given to AI
        //                        for(int i = e.offers.Count-1; i > 0; i--) {
        //                            if (e.offers[i].yours[1].CountStorables<Gold>() > e.offers[i].mine[0].Count && e.offers[i].TryApply()) {
        //                                e.offers[i].Apply();
        //                                Console.WriteLine(e.offers[i].you.name + " accepted offer of " + e.offers[i].me.name + "!");
        //                                e.offers.RemoveAt(i);
        //                            }
        //                        }
        //                    }
        //                    break;
        //                }
        //            case EntityType.Town: {
        //                    // Generate Food in towns                            
        //                    // randomly
        //                    if (ran.Next(10) > 6) {
        //                        // add 0 to 10 times pop. food
        //                        if (e.inventories[0].TryAdd<Food>(population + (ran.Next(5)+5) * (1+population))) {
        //                            e.inventories[0].Add<Food>(population + (ran.Next(5) + 5) * (1+population));
        //                        }
        //                    }
        //                    int foodsupply = e.inventories[0].CountStorables<Food>();
        //                    if (population < foodsupply/20) {
        //                        if (ran.Next(10) > 6) {
        //                            e.inventories[2].Add(new Man());
        //                        }
        //                    }

        //                    // offer visitors some food
        //                    foreach (Entity k in EntitiesInLocation(e.position)) {
        //                        // don't offer yourself
        //                        if (k == e) break;

        //                        // to do make offers
        //                        Offer o = new Offer(e, k, e.inventories, k.inventories);
        //                        k.offers.Add(o);
        //                        Console.WriteLine("Offer sent to " + o.you.name + " from " + o.me.name);
        //                    }


        //                    // Accept all offers given to AI
        //                    foreach (Offer o in e.offers) {
        //                        if (o.yours[0].CountStorables<Food>() <= e.inventories[0].CountStorables<Food>() && o.TryApply()) {
        //                            o.Apply();
        //                            e.offers.Remove(o);
        //                            Console.WriteLine(o.you.name + " accepted offer of " + o.me.name + "!");
        //                            break;
        //                        }
        //                    }
        //                    break;
        //                }
        //        }
        //        // consume food for each man in party
        //        for (int l = population-1; l > 0; l--) {
        //            // determine hunger of man
        //            int hunger = ran.Next(2) + 1;
        //            // get man
        //            if (e.inventories[2][l] is Man) {
        //                Man m = e.inventories[2][l] as Man;
        //                for (int k = 0; k < hunger; k++) {                            
        //                    // if there is food left
        //                    if (e.inventories[0].FindAll(p => p is Food).Count > 0) {
        //                        e.inventories[0].RemoveRange(0, 1);
        //                    } else {
        //                        if (m.Starve()) {
        //                            // man starved to death
        //                            e.inventories[2].Remove(m);
        //                            population--;
        //                        }
        //                    }
        //                }
        //                // while there is food left, feed the guy. Also,
        //                // dont feed the dead              

        //                while (m.Nutrition < m.Maxnutrition && m.Nutrition != 0 && e.inventories[0].FindAll(p => p is Food).Count > 0) {
        //                    e.inventories[0].FindAll(p => p is Food).RemoveRange(0, 1);
        //                    m.Feed();
        //                }
        //            }                    
        //        }

        //    }
        //    if (gtime == 2) {
        //        Hero ai = entities.Find(p => p.name == "Lord Hasenfuß") as Hero;
        //        ai.inventories[0].Add<Food>(10);
        //        Offer o = new Offer(ai, player, ai.inventories, player.inventories);
        //        player.offers.Add(o);
        //        Console.WriteLine("Offer sent to " + o.you.name + " from " + o.me.name);
        //    }
        //}

        //public static void PlayerInput(string input) {
        //    // MOVE
        //    if (input.Contains("move")) {
        //        bool validx = false, validy = false;
        //        int x = 0, y = 0;
        //        foreach (string s in input.Split(' ')) {
        //            if (!validx) {
        //                if (Int32.TryParse(s, out x)) {
        //                    validx = true;
        //                }
        //            } else {
        //                if (Int32.TryParse(s, out y)) {
        //                    validy = true;
        //                }
        //            }

        //        }
        //        if (validx && validy) {
        //            player.Move(new Vector2(x, y));
        //        } else {
        //            Console.WriteLine("No valid move");
        //        }
        //    }
        //    // VIEW
        //    else if (input.Contains("view")) {
        //        PrintEntities(entities);
        //    }
        //    // INVENTORY
        //    else if (input.Contains("inventory")) {
        //        Entity target = ChooseEntity();
        //        Console.WriteLine(target.name + "'s inventory contains:");
        //        target.inventories[0].PrintContents();
        //    }
        //    // KILL
        //    else if (input.Contains("kill")) {
        //        Entity target = ChooseEntity();
        //        Kill(target);
        //    }
        //    // GETTYPE
        //    else if (input.Contains("type")) {
        //        Entity target = ChooseEntity();
        //        Console.WriteLine("Type: " + target.GetType().ToString());
        //        Console.WriteLine("DeadHero? " + (target.GetType() == typeof(DeadHero) ? "true":"false"));
        //    }
        //    // ATTACK
        //    else if (input.Contains("attack")) {
        //        Console.WriteLine("Choose entity to attack:");
        //        Entity target = ChooseEntity();
        //        player.Attack(target);
        //    }
        //    // ADD FOOD
        //    else if (input.Contains("playeraddfood")) {
        //        Console.WriteLine("Add Food to Player.");
        //        if (player.inventories[0].TryAdd(new Food())) {
        //            player.inventories[0].Add(new Food());
        //        } else {
        //            Console.WriteLine("Can't add Food to Player.");
        //        }

        //    }
        //    // SEND OFFER
        //    else if (input.Contains("createoffer")) {
        //        Console.WriteLine("Create Offer.");
        //        Console.WriteLine("With whom?");
        //        Entity you = ChooseEntity();
        //        if (player.position.Equals(you.position)) {
        //            Offer offer = new Offer(player, you, ChooseItemsFromInventory(player), ChooseItemsFromInventory(you));
        //            you.offers.Add(offer);
        //            Console.WriteLine("Offer sent to " + offer.you.name + " from " + offer.me.name);
        //        } else {
        //            Console.WriteLine(you.name + " is not in range.");
        //        }
        //    }
        //    // ADD FOOD
        //    else if (input.Contains("addfood")) {
        //        player.inventories[0].Add(new Food());
        //    }
        //    // VIEW OFFER
        //    else if (input.Contains("voffer")) {
        //        Console.WriteLine("View Offer.");
        //        PrintOffers(player);
        //        Console.WriteLine("Enter offer hash to view:");
        //        string offerHash = Console.ReadLine();

        //        try {
        //            Offer o = ChooseOffer(offerHash, player);
        //            Console.WriteLine("Offer between " + o.me.name + " and " + o.you.name + ":");
        //            Console.WriteLine(o.me.name + " offers:");
        //            foreach (Inventory i in o.me.inventories) {
        //                i.PrintContents();
        //            }
        //            Console.WriteLine("for:");
        //            foreach (Inventory i in o.you.inventories) {
        //                i.PrintContents();
        //            }
        //        } catch (Exception) {
        //            Console.WriteLine("No valid Offer.");
        //        }
        //    }
        //    // ACCEPT OFFER
        //    else if (input.Contains("acceptoffer")) {
        //        Console.WriteLine("Accept Offer.");
        //        PrintOffers(player);
        //        Console.WriteLine("Enter offer hash to accept:");
        //        string offerHash = Console.ReadLine();
        //        try {
        //            Offer o = ChooseOffer(offerHash, player);
        //            if (o.TryApply()) {
        //                o.Apply();
        //                player.offers.Remove(o);
        //            }
        //        } catch (Exception) {
        //            Console.WriteLine("No valid Offer.");
        //        }
        //    }
        //    // REJECT OFFER
        //    else if (input.Contains("rejectoffer")) {
        //        Console.WriteLine("Reject Offer.");
        //        PrintOffers(player);
        //        Console.WriteLine("Enter offer hash to reject:");
        //        string offerHash = Console.ReadLine();
        //        try {
        //            Offer o = ChooseOffer(offerHash, player);
        //            if (o.TryApply()) {
        //                o.Apply();
        //                player.offers.Remove(o);
        //            }
        //        } catch (Exception) {
        //            Console.WriteLine("No valid offer.");
        //        }
        //    }
        //}

        //public static void PrintOffers(Entity e) {
        //    for (int i = 0; i < e.offers.Count; i++) {
        //        Console.WriteLine("Offer " + i + ": " + e.offers[i].GetHashCode());
        //    }
        //}

        //public static Offer ChooseOffer(string s, Entity e) {
        //    int i = -1;
        //    if (Int32.TryParse(s, out i) && i > -1 && i < player.offers.Count) {
        //        if (player.offers[i] != null) {
        //            return player.offers[i];
        //        } else {
        //            throw new Exception();
        //        }
        //    } else {
        //        foreach (Offer o in player.offers) {
        //            if (o.GetHashCode().ToString() == s) {
        //                return o;
        //            }
        //        }
        //    }
        //    throw new Exception();
        //}

        //public static Inventory[] ChooseItemsFromInventory(Entity source) {
        //    List<Inventory> invlist = new List<Inventory>();
        //    // choose which one to select from player
        //    foreach (Inventory inv in source.inventories) {
        //        Inventory clone = new Inventory(inv.Name, inv.Capacity);
        //        // get all hashcodes
        //        List<string> hashcodes = inv.ConvertAll<string>(p => p.GetHashCode().ToString());
        //        Console.WriteLine("Inventory " + inv.Name + " contents:");
        //        for (int i = 0; i < inv.Count; i++) {
        //            Console.WriteLine("item " + i + ": " + inv[i].GetType().ToString() + " " + hashcodes[i]);
        //        }
        //        Console.WriteLine("Enter item hashes or indices seperated by spaces:");
        //        string selection = Console.ReadLine();
        //        if (!string.IsNullOrEmpty(selection)) {
        //            string[] hashes = selection.Split(' ');
        //            for (int i = 0; i < hashes.Length; i++) {
        //                for (int j = 0; j < hashcodes.Count; j++) {
        //                    int tryint;
        //                    if (hashes[i] == hashcodes[j]) {
        //                        clone.Add(inv[j]);
        //                        break;
        //                    } else if (int.TryParse(hashes[i],out tryint) && tryint==j) {
        //                        clone.Add(inv[j]);
        //                        break;
        //                    }
        //                }
        //            }
        //        }                
        //        invlist.Add(clone);
        //    }           
        //    return invlist.ToArray();
        //}

        //public static Entity ChooseEntity() {
        //    // choose partner
        //    PrintEntities(entities);
        //    int id = -1;
        //    string partner = Console.ReadLine();
        //    if (!string.IsNullOrEmpty(partner) && Int32.TryParse(partner, out id) && entities[id] != null) {
        //        return entities[id];
        //    } else {
        //        throw new InvalidInputException(); 
        //    }
        //}

        //public static void PrintEntities(List<Entity> es) {
        //    foreach (Hero h in entities.FindAll(h => typeof(Hero) == h.GetType())) {
        //        Console.WriteLine("[" + entities.IndexOf(h) + "] " + h.name + " is at " + h.position.ToString());
        //        Console.WriteLine("And has " + h.inventories[0].CountStorables<Food>() + " food.");
        //        Console.WriteLine("And has " + h.inventories[1].CountStorables<Gold>() + " gold.");
        //        Console.WriteLine("And has " + h.inventories[2].CountStorables<Man>() + " men.");
        //    }
        //    foreach (Town t in entities.FindAll(t => typeof(Town) == t.GetType())) {
        //        if (true) {
        //            Console.WriteLine("[" + entities.IndexOf(t) + "] " + t.name + " is at " + t.position.ToString());
        //            Console.WriteLine("And has " + t.inventories[0].CountStorables<Food>() + " food.");
        //            Console.WriteLine("And has " + t.inventories[1].CountStorables<Gold>() + " gold.");
        //            Console.WriteLine("And has " + t.inventories[2].CountStorables<Man>() + " men.");
        //        }
        //    }
        //}

        //public static int GetId() {
        //    return lastId++;
        //}

        //public static void Kill(Entity e) {
        //    int index = entities.IndexOf(e);
        //    if (index > -1) {
        //        if (e.GetType() == typeof(Hero)) {
        //            entities.Insert(index, new DeadHero(e as Hero));
        //            entities.RemoveAt(index + 1);
        //        } else if (e.GetType() == typeof(Town)) {
        //            entities.Insert(index, new DeadTown(e as Town));
        //            entities.RemoveAt(index + 1);
        //        }
        //    }
        //}

        //public static string NumberToWords(int number)
        //{
        //    if (number == 0)
        //        return "zero";

        //    if (number < 0)
        //        return "minus " + NumberToWords(Math.Abs(number));

        //    string words = "";

        //    if ((number / 1000000) > 0)
        //    {
        //        words += NumberToWords(number / 1000000) + " million ";
        //        number %= 1000000;
        //    }

        //    if ((number / 1000) > 0)
        //    {
        //        words += NumberToWords(number / 1000) + " thousand ";
        //        number %= 1000;
        //    }

        //    if ((number / 100) > 0)
        //    {
        //        words += NumberToWords(number / 100) + " hundred ";
        //        number %= 100;
        //    }

        //    if (number > 0)
        //    {
        //        if (words != "")
        //            words += "and ";

        //        var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        //        var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        //        if (number < 20)
        //            words += unitsMap[number];
        //        else
        //        {
        //            words += tensMap[number / 10];
        //            if ((number % 10) > 0)
        //                words += "-" + unitsMap[number % 10];
        //        }
        //    }

        //    return words;
        //}

        //public static List<Entity> EntitiesInLocation(Vector2 where) {
        //    return entities.FindAll(k => k.position.Equals(where));
        //}

        //public static List<Vector2> ListPlaces() {
        //    List<Vector2> places = new List<Vector2>();
        //    foreach (Entity e in entities.FindAll(p => p.GetType() == typeof(Town))) {
        //        places.Add(e.position);
        //    }
        //    return places;
        //}
    }        
}
