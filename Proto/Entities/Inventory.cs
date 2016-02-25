using Proto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class Inventory
    {
        public int capacity;
        public string parent;
        private List<Storable> storables;

        public Inventory(int cap, string par) {
            parent = par;
            capacity = cap;
            storables = new List<Storable>();
        }

        public List<Storable> GetStorables() {
            return storables;
        }

        public bool AddStorable(Storable stor) {
            if (storables.Count < capacity)
            {
                storables.Add(stor);
                return true;
            }
            else {
                return false;
            }
        }

        public bool AddStorable(int count, Type t)
        {
            if (capacity - storables.Count >= count)
            {
                for (int i = 0; i < count; i++) {
                    if (t == typeof(Food))
                    {
                        storables.Add(new Food());
                    } else if (t == typeof(Man))
                    {
                        storables.Add(new Man());
                    }
                    else if (t == typeof(Gold))
                    {
                        storables.Add(new Gold());
                    }
                }                
                return true;
            }
            else {
                return false;
            }
        }

        public bool RemoveStorable(Storable stor) {
            if (stor != null)
            {
                storables.Remove(stor);
                return true;
            }
            else {
                return false;
            }
        }

        public bool RemoveStorable(Type t)
        {
            foreach (Storable s in storables)
            {
                if (s.GetType() == t)
                {
                    storables.Remove(s);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveStorable(Type t, int i) {
            for (int j = 0; j < i; j++) {
                foreach (Storable s in storables) {
                    if (s.GetType() == t) {
                        storables.Remove(s);
                        break;
                    }
                }
            }
            return true;
        }

        public bool TransferStorable(Storable stor, Inventory to)
        {
            if (stor != null && to.GetStorables().Count < to.capacity)
            {
                to.AddStorable(stor);
                storables.Remove(stor);
                return true;
            }
            else {
                return false;
            }
        }

        public bool TransferAll(Inventory to) {
            // while there is enough space in target inventory
            // for all storables
            if (storables.Count <= to.capacity - to.storables.Count) {
                while(storables.Count > 0)
                {
                    TransferStorable(storables[0], to);
                }
                return true;
            } else {
                return false;
            }
        }

        public bool TransferAllOfType(Type t,Inventory to)
        {
            // while there is enough space in target inventory
            // for all storables of type t
            if (SCount(t) <= to.capacity - to.storables.Count)
            {
                for (int i = 0; i < SCount(t); i++) {
                    foreach (Storable stor in storables) {
                        if (stor.GetType() == t)
                        {
                            if (TransferStorable(stor, to))
                            {
                                break;
                            }
                            else {
                                // there was no transfer
                                throw new Exception();
                            }                            
                        }
                    }
                }
                return true;
            }
            else {
                return false;
            }
        }

        public void UpdateConsumtion()
        {
            int consumes = 0;
            int starves = 0;

            // consume Food
            int demand = storables.OfType<Man>().Count() - new Random().Next(storables.OfType<Man>().Count()/4);
            for (int i = 0; i < demand; i++) {
                if (RemoveStorable(typeof(Food)))
                {
                    consumes++;
                }
                else {
                    RemoveStorable(typeof(Man));
                    starves++;
                }                
            }

            if (starves != 0) {
                Console.WriteLine(starves + " men starved.");
            }
            if (consumes != 0) {
                Console.WriteLine(consumes + " men cosumed food.");
            }
        }

        public string GetReport() {
            string s = "";
            //foreach (Storable stor in storables) {
            //    s += stor.GetName() + " ";
            //}
            s += "Men: " + SCount(typeof(Man)) + " ";
            s += "Food: " + SCount(typeof(Food)) + " ";
            s += "Gold: " + SCount(typeof(Gold));
            return s;
        }

        public int SCount() {
            int i = 0;
            foreach (Storable stor in storables) {
                i++;
            }
            return i;
        }

        public int SCount(Type t) {
            int i = 0;
            foreach (Storable stor in storables) {
                if (stor.GetType() == t) {
                    i++;
                }
            }
            return i;
        }

        public int FreeSlots() {
            return capacity - SCount();
        }
    }
}
