using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Storables
{
    abstract class Storable{
        public enum SType{
            Food, Gold,Man,Promise
        }

        public abstract Storable Clone();

        public static SType DetermineType(Type s) {
            if (s == typeof(Food)) {
                return SType.Food;
            } else if (s == typeof(Gold)) {
                return SType.Gold;
            } else if (s == typeof(Man)) {
                return SType.Man;
            } else if (s == typeof(Promise)) {
                return SType.Promise;
            } else {

                throw new InvalidTypeException();
            }
        }
    }
}
