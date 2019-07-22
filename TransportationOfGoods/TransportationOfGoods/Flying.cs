using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    abstract class Flying : Transport
    {
        public Flying(double loadCapacity, double maxDistance, double speed)
            : base(loadCapacity, maxDistance, speed)
        {
        }
        public Flying(double loadCapacity, double maxDistance, Engine engine)
            : base(loadCapacity, maxDistance, engine)
        {
        }
    }
}
