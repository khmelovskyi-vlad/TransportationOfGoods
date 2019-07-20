using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    interface IBusy
    {
        bool Busy { get; set; }
        int numTrip { get; set; }
        DateTime[] StartTime { get; set; }
        DateTime[] EndtTime { get; set; }
    }
}
