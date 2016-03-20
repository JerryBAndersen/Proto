using Proto.Entities;
using System;
using System.Collections.Generic;

namespace Proto
{
    class Program
    {
        static int lastId = 0;
        static List<Entity> entities;

        static void Main(string[] args)
        {
            entities = new List<Entity>();
            Town[] towns = new Town[64];
            for (int i = 0; i < towns.Length; i++) {
                towns[i] = new Town("Saint " + NumberToWords(GetId()),new Vector2(0,GetId()));
            }
            entities.AddRange(towns);

            Hero[] heroes = new Hero[6];
            for (int i = 0; i < heroes.Length; i++) {
                heroes[i] = new Hero("Lord " + NumberToWords(GetId()), 3, new Vector2());
            }
            entities.AddRange(heroes);

            while (Console.ReadKey().Key != ConsoleKey.Escape) {
                Console.WriteLine("Tick.");
                foreach (Hero h in entities.FindAll(h=>typeof(Hero) == h.GetType())) {
                    Console.WriteLine(h.name + " is at " + h.position.ToString());
                }
                foreach (Town t in entities.FindAll(t => typeof(Town) == t.GetType()))
                {
                    Console.WriteLine(t.name + " is at " + t.position.ToString());
                }
            }
        }

        public static int GetId() {
            return lastId++;
        }

        public static void Kill(Hero h) {
            entities[entities.IndexOf(h)] = new DeadHero(h);
        }
        public static void Kill(Town t) {
            entities[entities.IndexOf(t)] = new DeadTown(t);
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
