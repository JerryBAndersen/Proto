using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto.Storables
{
    class Gold : Storable
    {
        public override Storable Clone() {
            return new Gold();
        }
    }
}
