using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    abstract class Transport: IBusy
    {
        public Transport(double loadCapacity, double maxDistance, double speed)
        {
            LoadCapacity = loadCapacity;
            MaxDistance = maxDistance;
            Speed = speed;
        }
        public bool Busy { get; set; }
        public Driver Driver { get; set; }
        protected double Speed { set; get; }
        public double LoadCapacity { set; get; }
        public double MaxDistance { set; get; }
        public virtual DateTime Trasfer(double distance)
        {
            if(Driver == null)
            {
                throw new DriverNotFoundException();
            }
            var duration = TimeSpan.FromHours(distance/Speed);
            var date = DateTime.Now.Add(duration);
            Busy = true;
            Driver.Busy = true;
            return date;
        }
    }
}
