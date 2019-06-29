using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    class DriverNotFoundException: Exception
    {
        public DriverNotFoundException()
            :base("Sorry, don`t have driver")
        {

        }
    }
}
