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
            while (true)
            {
                var weight = ReadDouble("weigth");
                var distance = ReadDouble("distance");
                Console.WriteLine("If you want to deliver the parcel faster click Enter, if cheaper click another key");
                var speedOrEconom = Console.ReadKey(true);
                Console.WriteLine("If you want to deliver the parcel with company Engine click Enter, if other company click another key");
                var withEngineOrNot = Console.ReadKey(true);

                        var needTransport = transportPool.Transfer(weight, distance, withEngineOrNot, speedOrEconom);
                        Console.WriteLine(needTransport);
                        var key = ReadChar();
                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
            }
            Console.WriteLine("Bye");
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
            Console.WriteLine("If you don`t want to send a new parcel click Escape, otherwise, click anouther key");
            var key = Console.ReadKey(true);
            return key;

        }
    }
}
