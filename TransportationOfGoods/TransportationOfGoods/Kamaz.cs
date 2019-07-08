using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    class Kamaz : Truck
    {
        public Kamaz(double loadCapacity, double maxDistance, double speed)
            : base(loadCapacity, maxDistance, speed)
        {
        }
        public Kamaz(double loadCapacity, double maxDistance, double weightTransport, double power, double weightTransportWithParcel, double frictionCoefficient)
            : base(loadCapacity, maxDistance, weightTransport, power, weightTransport, frictionCoefficient)
        {
        }
    }
}
