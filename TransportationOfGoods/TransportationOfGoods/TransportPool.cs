using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    class TransportPool
    {
        public TransportPool()
        {
            transports = new Transport[] {
                new Gidroscutter(0.020, 23, 110),
                new Boat(1, 100, 90),
                new Shipe(10000, 5000, 60),
                new Airplane(100000, 25000, 1000),
                new Tank(20, 30, 20),
                new Kamaz(2, 600, 80),
                new Zil(1.5, 700, 90),
                new Mers(0.7, 1000, 150),
                new Kapeyka(0.5, 300, 60),
                new Motorbike(0.05, 7, 60),
                new Bike(0.03, 3, 7),
            };
            drivers = new Driver[]
            {
                new Driver("Valodya", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Mers), typeof(Kapeyka)}),
                new Driver("Valk", new Type[]{typeof(Motorbike),typeof(Bike), typeof(Airplane)}),
                new Driver("Wolf", new Type[]{typeof(Shipe),typeof(Boat), typeof(Gidroscutter), typeof(Bike),typeof(Kapeyka)}),
                new Driver("Vanya", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Mers), typeof(Kapeyka)}),
                new Driver("Vasya", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Mers), typeof(Kapeyka)}),
                new Driver("Viktir", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Mers), typeof(Kapeyka)}),
                new Driver("Vladislav", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Mers), typeof(Kapeyka)}),
                new Driver("Vlad", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Mers), typeof(Kapeyka)}),
            };
        }

        Transport[] transports;
        Driver[] drivers;

        public (string transportName, DateTime date) Tranfer(double weight, double distance)
        {
            var needTransportWeightToDistance = double.MaxValue;
            var weightToDistance = 0d;
            Transport needTransport = null;

            foreach (var necessaryTransports in transports)
            {
                var loadCapacity = necessaryTransports.LoadCapacity;
                var maxDistance = necessaryTransports.MaxDistance;
                if ( weight > loadCapacity || distance > maxDistance || necessaryTransports.Busy)
                {
                    continue;
                }
                else
                {
                    weightToDistance = necessaryTransports.LoadCapacity * necessaryTransports.MaxDistance;
                    if (weightToDistance < needTransportWeightToDistance)
                    {
                        needTransportWeightToDistance = weightToDistance;
                        needTransport = necessaryTransports;
                    }
                }
            }
            if (needTransport == null)
            {
                throw new TransortNotFoundException();
            }
            foreach(var driver in drivers)
            {
                if (driver.CanDrive(needTransport.GetType()))
                {
                    needTransport.Driver = driver;
                }
            }
            return (needTransport.GetType().Name, needTransport.Trasfer(distance));
        }
    }
}
