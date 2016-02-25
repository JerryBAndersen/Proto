using Proto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Entities
{
    class Food : Storable
    {
        public string GetName()
        {
            return "Food";
        }
    }
}
