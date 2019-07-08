using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    abstract class Engine
    {
        public Engine(double weightTransport, double power, double weightTransportWithParcel, double frictionCoefficient)
        {
            this.WeightTransport = weightTransport;
            this.Power = power;
            this.WeightTransportWithParcel = weightTransport;
            this.FrictionCoefficient = frictionCoefficient;
        }
        public double WeightTransport { set; get; }
        public double Power { set; get; }
        public double WeightTransportWithParcel { get; set; }
        public double FrictionCoefficient { get; set; }

    }
}
