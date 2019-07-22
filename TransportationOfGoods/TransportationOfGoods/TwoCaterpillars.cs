﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    abstract class TwoCaterpillars : Driving
    {
        public TwoCaterpillars(double loadCapacity, double maxDistance, double speed)
            : base(loadCapacity, maxDistance, speed)
        {
        }
        public TwoCaterpillars(double loadCapacity, double maxDistance, Engine engine)
            : base(loadCapacity, maxDistance, engine)
        {
        }
    }
}
