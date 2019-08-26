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
        }

        Transport[] transportsWithEngine;
        Transport[] transports;
        Driver[] drivers;
        Engine[] engines;
        DateTime departure;
        Transport needTransport;
        Transport[] extraArrayTransport;
        double weight;
        double distance;
        ConsoleKeyInfo withEngineOrNot;
        ConsoleKeyInfo speedOrEconom;
        int transportLength;
        Transport necessaryTransports;
        double needTransportWeightToDistance;
        double firstSpeed;
        double maxDistanceAllTransports;
        double maxLoadCapacityAllTransports;
        double maxSpeed;
        double weightToDistance;
        Transport[] needTransportArray;
        private void Departure()
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
            departure = dateOfDeparture;
        }
        private bool FindNotNeedTransportInArray()
        {
            if (transportLength != 0)
            {
                for (int i = 0; i < extraArrayTransport.Length; i++)
                {
                    if (necessaryTransports == extraArrayTransport[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void FindMaxDistanceAndMaxLoadCapacity ()
        {
            if (maxDistanceAllTransports < necessaryTransports.MaxDistance)
            {
                maxDistanceAllTransports = necessaryTransports.MaxDistance;
            }
            if (maxLoadCapacityAllTransports < necessaryTransports.LoadCapacity)
            {
                maxLoadCapacityAllTransports = necessaryTransports.LoadCapacity;
            }
        }
        private bool NecessaryTransportsBusy()
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
        private Transport FindTranport()
        {
            var havingTransport = false;
            var flagToTransport = false;
            FindNeedArgument();
            foreach (var necessaryTransports in needTransportArray)
            {
                this.necessaryTransports = necessaryTransports;
                FindMaxDistanceAndMaxLoadCapacity();
                havingTransport = FindNotNeedTransportInArray();
                flagToTransport = NecessaryTransportsBusy();
                if (weight > necessaryTransports.LoadCapacity || distance > necessaryTransports.MaxDistance || flagToTransport || havingTransport)
                {
                    continue;
                }
                if (withEngineOrNot.Key == ConsoleKey.Enter)
                {
                        FindNeedTransportsWithEngine();
                }
                else
                {
                        FindNeedTransports();
                }
            }
            BigParcel();
            return (needTransport);
        }
        private void FindNeedArgument()
        {
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
                needTransportArray = transportsWithEngine;
            }
            else
            {
                needTransportArray = transports;
            }
        }
        private void FindNeedTransportsWithEngine()
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
        }
        private void FindNeedTransports()
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
        }
        private void BigParcel()
        {
            if (maxDistanceAllTransports < distance || maxLoadCapacityAllTransports < weight)
            {
                Console.WriteLine("We have no transport, contact another organization, or split the parcel into several");
                throw new OperationCanceledException();
            }
        }
        private bool NotNeedTransport()
        {
            if (needTransport == null)
            {
                Console.WriteLine("We don`t have transport to this date, enter new pleasy");
                return true;
            }
            return false;
        }
        private bool NeedDriverBusy(Driver driver)
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
        private bool FindDriver()
        {
            var havingDriver = false;
            foreach (var driver in drivers)
            {
                if (driver.CanDrive(needTransport.GetType()))
                {
                    needTransport.Driver = driver;
                    havingDriver = NeedDriverBusy(driver);
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
        private void NotNeedDriver()
        {
                if (transportLength < needTransportArray.Length)
                {
                    extraArrayTransport[transportLength] = needTransport;
                }
                else
                {
                    transportLength = 0;
                    Console.WriteLine("We don`t have driver to this date, enter new pleasy");
                    return;
                }
                transportLength++;
        }
        private void DepartureNowOrLate()
        {
            Console.WriteLine("If you want to send the parcel now, press Enter, if you want to send the parcel later, click on another click");
            var key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Enter)
            {
                departure = DateTime.Now;
                return;
            }
            Departure();
        }
        private void FindExtraArrayTransport()
        {
            if (withEngineOrNot.Key == ConsoleKey.Enter)
            {
                extraArrayTransport = new Transport[transportsWithEngine.Length];
            }
            else
            {
                extraArrayTransport = new Transport[transports.Length];
            }
        }
        public (string transportName, DateTime date) Transfer(double weightTransfer, double distanceTransfer, ConsoleKeyInfo withEngineOrNotTransfer, ConsoleKeyInfo speedOrEconomTransfer)
        {
            DepartureNowOrLate();
            var haveNotNeedTransport = false;
            var haveNotNeedDriver = false;
            var havingDriver = false;
            weight = weightTransfer;
            distance = distanceTransfer;
            withEngineOrNot = withEngineOrNotTransfer;
            speedOrEconom = speedOrEconomTransfer;
            FindExtraArrayTransport();
            transportLength = 0;
            while (true)
            {
                needTransport = FindTranport();
                haveNotNeedTransport = NotNeedTransport();
                if (haveNotNeedTransport)
                {
                    Departure();
                    continue;
                }
                havingDriver = FindDriver();
                haveNotNeedDriver = NotNeedTransport();
                if (havingDriver)
                {
                    break;
                }
                else
                {
                    NotNeedDriver();
                    if (transportLength == 0)
                    {
                        Departure();
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
