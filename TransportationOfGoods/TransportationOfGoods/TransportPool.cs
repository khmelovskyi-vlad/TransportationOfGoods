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
                new Shipe (10000, 5000, 4000, 1000, 0, 0.2),
                new Gidroscutter(0.020, 23, 0.05, 10, 0, 0.2),
                new Boat(1, 100, 0.11, 15, 0, 0.2),
                new Airplane(20000, 25000, 900, 12000, 0, 0.09),
                new Tank(20, 30, 70, 700, 0, 0.1),
                new Kamaz(2, 600, 4.2, 154, 0, 0.1),
                new Zil(1.5, 700, 3.7, 110, 0, 0.1),
                new Mers(0.7, 1000, 1.8, 150, 0, 0.1),
                new Kapeyka(0.5, 300, 1.3, 50, 0, 0.1),
                new Motorbike(0.05, 7, 0.15, 20, 0, 0.1),
                new Bike(0.03, 3, 0.02, 0.001, 0, 0.1),
            };
            drivers = new Driver[]
            {
                new Driver("Valodya", new Type[]{typeof(Zil),typeof(Kamaz), typeof(Airplane), typeof(Mers), typeof(Kapeyka), typeof(Shipe)}),
            };
        }

        Transport[] transportsWithEngine;
        Transport[] transports;
        Driver[] drivers;
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
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bed input {ex.Message}, try again or click escape");
                }
            } while (true);
            return dateOfDeparture;
        }

        public (string transportName, DateTime date) Tranfer(double weight, double distance, int numParcel)
        {
            var needTransportWeightToDistance = double.MaxValue;
            var weightToDistance = 0d;
            Transport needTransport = null;
            int transportLength = 0;
            var havingDriver = false;
            var havingTransport = false;
            var maxDistanceAllTransports = 0d;
            var maxLoadCapacityAllTransports = 0d;
            Transport[] needTransportMatrix = new Transport[transports.Length];
            var departure = Departure();
            bool flagToTransport;
            bool flagToDriver;
            while (true){
                foreach (var necessaryTransports in transports)
                {
                    flagToTransport = false;
                    if (maxDistanceAllTransports < necessaryTransports.MaxDistance)
                    {
                        maxDistanceAllTransports = necessaryTransports.MaxDistance;
                    }
                    if (maxLoadCapacityAllTransports < necessaryTransports.LoadCapacity)
                    {
                        maxLoadCapacityAllTransports = necessaryTransports.LoadCapacity;
                    }
                    if (transportLength!=0)
                    {
                        for (int i = 0; i < needTransportMatrix.Length; i++)
                        {
                            if (necessaryTransports == needTransportMatrix[i])
                            {
                                havingTransport = true;
                                break;
                            }
                            else
                            {
                                havingTransport = false;
                            }
                        }
                    }
                    if (necessaryTransports.Busy)
                    {
                        var arrival = necessaryTransports.Arrival(distance, departure);
                        for (int i = 0; i < necessaryTransports.numTrip; i++)
                        {
                            if ((departure >= necessaryTransports.StartTime[i] && departure <= necessaryTransports.EndtTime[i]) || (arrival >= necessaryTransports.StartTime[i] && arrival <= necessaryTransports.EndtTime[i]) || (departure <= necessaryTransports.StartTime[i] && arrival >= necessaryTransports.EndtTime[i]))
                            {
                                flagToTransport = true;
                                break;
                            }
                        }
                    }
                    if (weight > necessaryTransports.LoadCapacity || distance > necessaryTransports.MaxDistance || flagToTransport || havingTransport==true)
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
                if (transportLength == transportsWithEngine.Length)
                {
                    if (maxDistanceAllTransports < distance || maxLoadCapacityAllTransports < weight)
                    {
                        Console.WriteLine("We have no transport, contact another organization, or split the parcel into several");
                        throw new OperationCanceledException();
                    }
                }
                if (needTransport == null)
                {
                    Console.WriteLine("We don`t have transport to this date, enter new pleasy");
                    departure = Departure();
                    continue;
                }
                foreach (var driver in drivers)
                {
                    if (driver.CanDrive(needTransport.GetType()))
                    {
                        flagToDriver = false;
                        var arrival = needTransport.Arrival(distance, departure);
                        needTransport.Driver = driver;
                        if (driver.Busy)
                        {
                            for (int i = 0; i < needTransport.Driver.numTrip; i++)
                            {

                                if ((departure >= driver.StartTime[i] && departure <= driver.EndtTime[i]) || (arrival >= driver.StartTime[i] && arrival <= driver.EndtTime[i]) || (departure <= driver.StartTime[i] && arrival >= driver.EndtTime[i]))
                                {
                                    flagToDriver = true;
                                    break;
                                }
                            }
                            if (flagToDriver == false)
                            {
                                havingDriver = true;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            havingDriver = true;
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if( havingDriver == true)
                {
                    break;
                }
                else
                {
                    weightToDistance = 0;
                    needTransportWeightToDistance = double.MaxValue;
                    try
                    {
                        needTransportMatrix[transportLength] = needTransport;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("We don`t have driver to this date, enter new pleasy");
                        departure = Departure();
                        continue;
                    }
                    transportLength++;
                    continue;
                }
            }
            var needArrival = needTransport.Arrival(distance, departure);
            needTransport.Trasfer(departure, needArrival, numParcel);
            return (needTransport.GetType().Name, needArrival);
        }
        public (string transportName, DateTime date) TransferTrinsportsWithEngine(double weight, double distance)
        {
            var firstSpeed = 0d;
            var maxSpeed = 0d;
            Transport needTransport = null;
            int l = 0;
            var havingDriver = false;
            var havingTransport = false;
            var maxDistanceAllTransports = 0d;
            var maxLoadCapacityAllTransports = 0d;
            var transportLength = 0;
            Transport[] needTransportMatrix = new Transport[transportsWithEngine.Length];
            while (true)
            {
                foreach (var necessaryTransports in transportsWithEngine)
                {
                    if (maxDistanceAllTransports < necessaryTransports.MaxDistance)
                    {
                        maxDistanceAllTransports = necessaryTransports.MaxDistance;
                    }
                    if (maxLoadCapacityAllTransports < necessaryTransports.LoadCapacity)
                    {
                        maxLoadCapacityAllTransports = necessaryTransports.LoadCapacity;
                    }
                    transportLength++;
                    if (l != 0)
                    {
                        for (int i = 0; i < needTransportMatrix.Length; i++)
                        {
                            if (necessaryTransports == needTransportMatrix[i])
                            {
                                havingTransport = true;
                                break;
                            }
                            else
                            {
                                havingTransport = false;
                            }
                        }
                    }
                    if (weight > necessaryTransports.LoadCapacity || distance > necessaryTransports.MaxDistance || necessaryTransports.Busy || havingTransport == true)
                    {
                        continue;
                    }
                    else
                    {
                        necessaryTransports.WeightTransportWithParcel = necessaryTransports.WeightTransport + weight;
                        maxSpeed = necessaryTransports.Power / (necessaryTransports.WeightTransportWithParcel * necessaryTransports.FrictionCoefficient * 0.0098);
                        if (maxSpeed > firstSpeed)
                        {
                            firstSpeed = maxSpeed;
                            needTransport = necessaryTransports;
                        }
                    }
                }
                if (transportLength == transportsWithEngine.Length)
                {
                    if (maxDistanceAllTransports < distance || maxLoadCapacityAllTransports < weight)
                    {
                        Console.WriteLine("We have no transport, contact another organization, or split the parcel into several");
                        throw new OperationCanceledException();
                    }
                }
                if (needTransport == null)
                {
                    throw new TransortNotFoundException();
                }
                foreach (var driver in drivers)
                {
                    if (driver.CanDrive(needTransport.GetType()))
                    {
                        needTransport.Driver = driver;
                        if (needTransport.Driver.Busy)
                        {
                            continue;
                        }
                        havingDriver = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (havingDriver == true)
                {
                    break;
                }
                else
                {
                    firstSpeed = 0d;
                    maxSpeed = 0d;
                    try
                    {
                        needTransportMatrix[l] = needTransport;
                    }
                    catch (Exception)
                    {
                        throw new DriverNotFoundException();
                    }
                    l++;
                    continue;
                }
            }
            return (needTransport.GetType().Name, needTransport.TransferTransortEngine(distance));
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
