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
                new Shipe(10000, 5000, 60),
                new Gidroscutter(0.020, 23, 110),
                new Boat(1, 100, 90),
                new Airplane(100000, 25000, 1000),
                new Tank(20, 30, 20),
                new Kamaz(2, 600, 80),
                new Zil(1.5, 700, 90),
                new Mers(0.7, 1000, 150),
                new Kapeyka(0.5, 300, 60),
                new Motorbike(0.05, 7, 60),
                new Bike(0.03, 3, 7),
            };
            transportsWithEngine = new Transport[]
            {
                new Shipe (10000, 5000, new Engine(4000, 1000, 0, 0.2)),
                new Gidroscutter(0.020, 23, new Engine(0.05, 10, 0, 0.2)),
                new Boat(1, 100, new Engine(0.11, 15, 0, 0.2)),
                new Airplane(20000, 25000, new Engine(900, 12000, 0, 0.09)),
                new Tank(20, 30, new Engine(70, 700, 0, 0.1)),
                new Kamaz(2, 600, new Engine(4.2, 154, 0, 0.1)),
                new Zil(1.5, 700, new Engine(3.7, 110, 0, 0.1)),
                new Mers(0.7, 1000, new Engine(1.8, 150, 0, 0.1)),
                new Kapeyka(0.5, 300, new Engine(1.3, 50, 0, 0.1)),
                new Motorbike(0.05, 7, new Engine(0.15, 20, 0, 0.1)),
                new Bike(0.03, 3, new Engine(0.02, 0.001, 0, 0.1)),
            };
            drivers = new Driver[]
            {
                new Driver("Valodya", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Airplane), typeof(Mers), typeof(Kapeyka), typeof(Shipe)}),
                new Driver("Valodya", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Airplane), typeof(Mers), typeof(Kapeyka), typeof(Shipe)}),
            };
            engines = new Engine[]
            {
                new Engine(0.11, 15, 0, 0.2),
                new Engine(11111111, 1500000, 100000, 10000),
            };
        }

        Transport[] transportsWithEngine;
        Transport[] transports;
        Driver[] drivers;
        Engine[] engines;
        public virtual DateTime Departure()
        {
            DateTime dateOfDeparture = new DateTime();
            do
            {
                try
                {
                    int year = ReadInt("year");
                    int month = ReadInt("month");
                    int day = ReadInt("day");
                    int hour = ReadInt("hour");
                    int minute = ReadInt("minute");
                    dateOfDeparture = new DateTime(year, month, day, hour, minute, 0);
                    if(dateOfDeparture < DateTime.Now)
                    {
                        Console.WriteLine("Date has passed, please enter another date");
                        continue;
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bed input {ex.Message}, try again or click escape");
                }
            } while (true);
            return dateOfDeparture;
        }
        public bool FindNotNeedTransportInArray(Transport[] needTransportArray, int transportLength, Transport necessaryTransports)
        {
            if (transportLength != 0)
            {
                for (int i = 0; i < needTransportArray.Length; i++)
                {
                    if (necessaryTransports == needTransportArray[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public (double maxDistanceAllTransports, double maxLoadCapacityAllTransports) FindMaxDistanceAndMaxLoadCapacity (Transport necessaryTransports, double maxDistanceAllTransports, double maxLoadCapacityAllTransports)
        {

            if (maxDistanceAllTransports < necessaryTransports.MaxDistance)
            {
                maxDistanceAllTransports = necessaryTransports.MaxDistance;
            }
            if (maxLoadCapacityAllTransports < necessaryTransports.LoadCapacity)
            {
                maxLoadCapacityAllTransports = necessaryTransports.LoadCapacity;
            }
            return (maxDistanceAllTransports, maxLoadCapacityAllTransports);
        }
        public bool NecessaryTransportsBusy(DateTime departure, Transport necessaryTransports, double distance, ConsoleKeyInfo withEngineOrNot)
        {
            if (necessaryTransports.Busy)
            {
                var arrival = necessaryTransports.Arrival(distance, departure, withEngineOrNot);
                for (int i = 0; i < necessaryTransports.numTrip; i++)
                {
                    if ((departure >= necessaryTransports.StartTime[i] && departure <= necessaryTransports.EndtTime[i]) || (arrival >= necessaryTransports.StartTime[i] && arrival <= necessaryTransports.EndtTime[i]) || (departure <= necessaryTransports.StartTime[i] && arrival >= necessaryTransports.EndtTime[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public Transport FindTranport(double weight, double distance, DateTime departure, Transport needTransport, Transport[] needTransportArray, int transportLength, ConsoleKeyInfo withEngineOrNot, ConsoleKeyInfo speedOrEconom)
        {
            var maxDistanceAllTransports = 0d;
            var maxLoadCapacityAllTransports = 0d;
            var havingTransport = false;
            var flagToTransport = false;
            double firstSpeed = 0;
            var maxSpeed = 0d;
            var weightToDistance = 0d;
            double needTransportWeightToDistance = 0;
            Transport[] transportsOrTransportsWithEngine;
            (needTransportWeightToDistance, firstSpeed, transportsOrTransportsWithEngine) = FindNeedArgument(withEngineOrNot, speedOrEconom);
            foreach (var necessaryTransports in transportsOrTransportsWithEngine)
            {
                (maxDistanceAllTransports, maxLoadCapacityAllTransports) = FindMaxDistanceAndMaxLoadCapacity(necessaryTransports, maxDistanceAllTransports, maxLoadCapacityAllTransports);
                havingTransport = FindNotNeedTransportInArray(needTransportArray, transportLength, necessaryTransports);
                flagToTransport = NecessaryTransportsBusy(departure, necessaryTransports, distance, withEngineOrNot);
                if (weight > necessaryTransports.LoadCapacity || distance > necessaryTransports.MaxDistance || flagToTransport || havingTransport)
                {
                    continue;
                }
                if (withEngineOrNot.Key == ConsoleKey.Enter)
                {
                        (firstSpeed, needTransport) = FindNeedTransportsWithEngine(necessaryTransports, needTransport, maxSpeed, firstSpeed, weight, speedOrEconom);
                }
                else
                {
                        (needTransportWeightToDistance, needTransport) = FindNeedTransports(necessaryTransports, needTransport, weightToDistance, needTransportWeightToDistance, speedOrEconom);
                }
            }
            BigParcel(maxDistanceAllTransports, maxLoadCapacityAllTransports, weight, distance);
            return (needTransport);
        }
        public (double needTransportWeightToDistance, double firstSpeed, Transport[] transportsOrTransportsWithEngine) FindNeedArgument(ConsoleKeyInfo withEngineOrNot, ConsoleKeyInfo speedOrEconom)
        {
            double needTransportWeightToDistance;
            double firstSpeed;
            Transport[] transportsOrTransportsWithEngine;
            if (speedOrEconom.Key == ConsoleKey.Enter)
            {
                needTransportWeightToDistance = 0d;
                firstSpeed = 0d;
            }
            else
            {
                needTransportWeightToDistance = double.MaxValue;
                firstSpeed = double.MaxValue;
            }
            if (withEngineOrNot.Key == ConsoleKey.Enter)
            {
                transportsOrTransportsWithEngine = transportsWithEngine;
            }
            else
            {
                transportsOrTransportsWithEngine = transports;
            }
            return (needTransportWeightToDistance, firstSpeed, transportsOrTransportsWithEngine);
        }
        public (double firstSpeed, Transport needTransport) FindNeedTransportsWithEngine(Transport necessaryTransports, Transport needTransport, double maxSpeed, double firstSpeed, double weight, ConsoleKeyInfo speedOrEconom)
        {
            necessaryTransports.Engine.WeightTransportWithParcel = necessaryTransports.Engine.WeightTransport + weight;
            maxSpeed = necessaryTransports.Engine.Power / (necessaryTransports.Engine.WeightTransportWithParcel * necessaryTransports.Engine.FrictionCoefficient * 0.0098);
            if (speedOrEconom.Key == ConsoleKey.Enter)
            {
                if (maxSpeed > firstSpeed)
                {
                    firstSpeed = maxSpeed;
                    needTransport = necessaryTransports;
                }
            }
            else
            {
                if (maxSpeed < firstSpeed)
                {
                    firstSpeed = maxSpeed;
                    needTransport = necessaryTransports;
                }
            }
            return (firstSpeed, needTransport);
        }
        public (double needTransportWeightToDistance, Transport needTransport) FindNeedTransports(Transport necessaryTransports, Transport needTransport, double weightToDistance, double needTransportWeightToDistance, ConsoleKeyInfo speedOrEconom)
        {
            weightToDistance = necessaryTransports.LoadCapacity * necessaryTransports.MaxDistance;
            if (speedOrEconom.Key == ConsoleKey.Enter)
            {
                if (weightToDistance > needTransportWeightToDistance)
                {
                    needTransportWeightToDistance = weightToDistance;
                    needTransport = necessaryTransports;
                }
            }
            else
            {
                if (weightToDistance < needTransportWeightToDistance)
                {
                    needTransportWeightToDistance = weightToDistance;
                    needTransport = necessaryTransports;
                }
            }
            return (needTransportWeightToDistance, needTransport);
        }
        public void BigParcel(double maxDistanceAllTransports, double maxLoadCapacityAllTransports, double distance, double weight)
        {
            if (maxDistanceAllTransports < distance || maxLoadCapacityAllTransports < weight)
            {
                Console.WriteLine("We have no transport, contact another organization, or split the parcel into several");
                throw new OperationCanceledException();
            }
        }
        public bool NotNeedTransport(Transport needTransport, DateTime departure)
        {
            if (needTransport == null)
            {
                Console.WriteLine("We don`t have transport to this date, enter new pleasy");
                return true;
            }
            return false;
        }
        public bool NeedDriverBusy(Driver driver, Transport needTransport, DateTime departure, double distance, ConsoleKeyInfo withEngineOrNot)
        {
            if (driver.Busy)
            {
                for (int i = 0; i < needTransport.Driver.numTrip; i++)
                {
                    var arrival = needTransport.Arrival(distance, departure, withEngineOrNot);
                    if ((departure >= driver.StartTime[i] && departure <= driver.EndtTime[i]) || (arrival >= driver.StartTime[i] && arrival <= driver.EndtTime[i]) || (departure <= driver.StartTime[i] && arrival >= driver.EndtTime[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool FindDriver(Transport needTransport, DateTime departure, double distance, ConsoleKeyInfo withEngineOrNot)
        {
            var havingDriver = false;
            foreach (var driver in drivers)
            {
                if (driver.CanDrive(needTransport.GetType()))
                {
                    needTransport.Driver = driver;
                    havingDriver = NeedDriverBusy(driver, needTransport, departure, distance, withEngineOrNot);
                    if (havingDriver)
                    {
                        return true;
                    }
                }
                else
                {
                    continue;
                }
            }
            return false;
        }
        public int NotNeedDriver(Transport needTransport, DateTime departure, Transport[] needTransportArray, int transportLength, ConsoleKeyInfo withEngineOrNot)
        {
            Transport[] findTransports;
            if (withEngineOrNot.Key == ConsoleKey.Enter)
            {
                findTransports = transportsWithEngine;
            }
            else
            {
                findTransports = transports;
            }
                if (transportLength < findTransports.Length)
                {
                    needTransportArray[transportLength] = needTransport;
                }
                else
                {
                    transportLength = 0;
                    Console.WriteLine("We don`t have driver to this date, enter new pleasy");
                    return transportLength;
                }
                transportLength++;
                return transportLength;
        }
        public DateTime DepartureNowOrLate()
        {
            Console.WriteLine("If you want to send the parcel now, press Enter, if you want to send the parcel later, click on another click");
            var key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Enter)
            {
                return DateTime.Now;
            }
            return Departure();
        }
        public (string transportName, DateTime date) Transfer(double weight, double distance, ConsoleKeyInfo withEngineOrNot, ConsoleKeyInfo speedOrEconom)
        {
            var departure = DepartureNowOrLate();
            Transport needTransport = null;
            var haveNotNeedTransport = false;
            var haveNotNeedDriver = false;
            var havingDriver = false;
            Transport[] needTransportArray;
            if (withEngineOrNot.Key == ConsoleKey.Enter)
            {
                needTransportArray = new Transport[transportsWithEngine.Length];
            }
            else
            {
                needTransportArray = new Transport[transports.Length];
            }
            var transportLength = 0;
            while (true)
            {
                needTransport = FindTranport(weight, distance, departure, needTransport, needTransportArray, transportLength, withEngineOrNot, speedOrEconom);
                haveNotNeedTransport = NotNeedTransport(needTransport, departure);
                if (haveNotNeedTransport)
                {
                    departure = Departure();
                    continue;
                }
                havingDriver = FindDriver(needTransport, departure, distance, withEngineOrNot);
                haveNotNeedDriver = NotNeedTransport(needTransport, departure);
                if (havingDriver)
                {
                    break;
                }
                else
                {
                    transportLength = NotNeedDriver(needTransport, departure, needTransportArray, transportLength, withEngineOrNot);
                    if (transportLength == 0)
                    {
                        departure = Departure();
                    }
                }
            }
            var needArrival = needTransport.Arrival(distance, departure, withEngineOrNot);
            needTransport.Trasfer(departure, needArrival);
            return (needTransport.GetType().Name, needArrival);
        }
        static int ReadInt(string readInt)
        {
            do
            {
                try
                {
                    Console.WriteLine($"Enter {readInt}");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        throw new OperationCanceledException();
                    }
                    var line = Console.ReadLine();
                    var keyLine = $"{key.KeyChar}{line}";
                    return Convert.ToInt32((keyLine));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Bed input {ex.Message}, try again or click escape");
                }
            } while (true);
        }
    }
}
