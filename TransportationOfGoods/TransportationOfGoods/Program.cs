using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sending a parcels");
            TransportPool transportPool = new TransportPool();
            var numParcel = 0;
            while (true)
            {
                numParcel++;
                var weight = ReadDouble("weigth");
                var distance = ReadDouble("distance");
                Console.WriteLine("If you want to deliver the parcel faster click Enter, if cheaper click another key");
                var speedOrEconom = Console.ReadKey(true);
                if (speedOrEconom.Key == ConsoleKey.Enter)
                {
                    try
                    {
                        var needTransport = transportPool.Test2(weight, distance, numParcel, speedOrEconom);
                        Console.WriteLine(needTransport);
                        var key = ReadChar();
                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                    catch (TransortNotFoundException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        var needTransport = transportPool.Test2(weight, distance, numParcel, speedOrEconom);
                        var key = ReadChar();
                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    try
                    {
                        var needTransport = transportPool.Test(weight, distance, numParcel, speedOrEconom);
                        Console.WriteLine(needTransport);
                        var key = ReadChar();
                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                    catch (TransortNotFoundException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        var needTransport = transportPool.Test(weight, distance, numParcel, speedOrEconom);
                        var key = ReadChar();
                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                }
            }

            Console.ReadKey();
        }
        static double ReadDouble (string readInt)
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
                    return Convert.ToDouble((keyLine));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Bed input {ex.Message}, try again or click escape");
                }
            } while (true);
        }
        static ConsoleKeyInfo ReadChar()
        {
            Console.WriteLine("If you want to send a new parcel click Enter, otherwise, click Escape");
            var key = Console.ReadKey(true);
            return key;

        }
    }
}
