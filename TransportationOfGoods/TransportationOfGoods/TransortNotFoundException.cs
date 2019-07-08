using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    class TransortNotFoundException : Exception
    {
        public TransortNotFoundException()
            :base("Situable transport don't found")
        {

        }
    }
}
