using Proto.Entities;
using Proto.Storables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Misc
{
    class Offer {
        public Entity me, you;
        public Inventory[] mine, yours, mytemp, yourtemp;

        public Offer(Entity me, Entity you, Inventory[] mine, Inventory[] yours) {
            this.me = me;
            this.you = you;
            this.mine = mine;
            this.yours = yours;

            mytemp = new Inventory[4];
            mytemp[0] = new Inventory("storage", Int32.MaxValue);
            mytemp[1] = new Inventory("money", Int32.MaxValue);
            mytemp[2] = new Inventory("party", Int32.MaxValue);
            mytemp[3] = new Inventory("promises", Int32.MaxValue);

            yourtemp = new Inventory[4];
            yourtemp[0] = new Inventory("storage", Int32.MaxValue);
            yourtemp[1] = new Inventory("money", Int32.MaxValue);
            yourtemp[2] = new Inventory("party", Int32.MaxValue);
            yourtemp[3] = new Inventory("promises", Int32.MaxValue);
        }

        public bool TryApply() {
            // check if the offer could be applied
            for (int i = 0; i < mine.Length; i++) {
                if (!(me.inventories[i].TryRemove(mine[i]) && you.inventories[i].FreeCapacity >= mine.Length)) {
                    return false;
                }
            }
            for (int i = 0; i < mine.Length; i++) {
                if (!(you.inventories[i].TryRemove(yours[i]) && me.inventories[i].FreeCapacity >= yours.Length)) {
                    return false;
                }
            }
            return true;
        }

        public void Apply() {
            // for each inventory in my offer
            for (int i = 0; i < mine.Length; i++) {
                if (mine[i].TryTransferAll(mytemp[i])) {
                    mine[i].TransferAll(mytemp[i]);
                } else {
                    throw new Exception();
                }
            }
            // for each inventory in your offer
            for (int i = 0; i < yours.Length; i++) {
                if (yours[i].TryTransferAll(yourtemp[i])) {
                    yours[i].TransferAll(yourtemp[i]);
                } else {
                    throw new Exception();
                }
            }

            // for each inventory in my temp
            for (int i = 0; i < mine.Length; i++) {
                if (mytemp[i].TryTransferAll(you.inventories[i])) {
                    mytemp[i].TransferAll(you.inventories[i]);
                } else {
                    throw new Exception();
                }
            }
            // for each inventory in your temp
            for (int i = 0; i < yours.Length; i++) {
                if (yourtemp[i].TryTransferAll(me.inventories[i])) {
                    yourtemp[i].TransferAll(me.inventories[i]);
                } else {
                    throw new Exception();
                }
            }
        }

        public void Reject() {
            Console.WriteLine("Offer from " + me.name + " to " + you.name + " rejected!");
            you.offers.Remove(this);
        }

    }
}
