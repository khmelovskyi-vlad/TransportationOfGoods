using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    abstract class Transport : Engine, IBusy
    {
        public Transport(double loadCapacity, double maxDistance, double speed)
            :base(0,0,0,0)
        {
            LoadCapacity = loadCapacity;
            MaxDistance = maxDistance;
            Speed = speed;
        }
        public Transport(double loadCapacity, double maxDistance, double weightTransport, double power, double weightTransportWithParcel, double frictionCoefficient)
            :base(weightTransport, power, weightTransport, frictionCoefficient)
        {
            LoadCapacity = loadCapacity;
            MaxDistance = maxDistance;
        }
        public bool Busy { get; set; }
        public Driver Driver { get; set; }
        public double LoadCapacity { set; get; }
        public double MaxDistance { set; get; }
        protected double Speed { set; get; }
        public virtual DateTime Trasfer(double distance)
        {
            DateTime dateOfDeparture = new DateTime();
            DateTime dateOfArrival = new DateTime();
            if (Driver == null)
            {
                throw new DriverNotFoundException();
            }
            var duration = TimeSpan.FromHours(distance / Speed);

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
                    dateOfArrival = new DateTime(year, month, day, hour, minute, 0).Add(duration);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Bed input {ex.Message}, try again or click escape");
                    }
                } while (true);
            Driver.Busy = true;
            Busy = true;
            return dateOfArrival;
        }
        public virtual DateTime TransferTransortEngine(double distance)
        {
            DateTime dateOfDeparture = new DateTime();
            DateTime dateOfArrival = new DateTime();
            if (Driver == null)
            {
                throw new DriverNotFoundException();
            }
            var l = Power / (WeightTransportWithParcel * FrictionCoefficient * 0.0098);
            var duration = TimeSpan.FromHours(distance / (l));

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
                    dateOfArrival = new DateTime(year, month, day, hour, minute, 0).Add(duration);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bed input {ex.Message}, try again or click escape");
                }
            } while (true);
            Driver.Busy = true;
            Busy = true;
            return dateOfArrival;
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
