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
            TransportPool transportPool = new TransportPool();
            try
            {
                var needTransport = transportPool.Tranfer(50, 8);
                Console.WriteLine(needTransport);
            }
            catch(TransortNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
