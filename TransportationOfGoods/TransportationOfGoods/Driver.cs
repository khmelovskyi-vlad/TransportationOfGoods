using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOfGoods
{
    class Driver: IBusy
    {
        public Driver(string name, Type[] skills)
        {
            this.Name = name;
            this.Skills = skills;
        }
        public string Name { get; set; }
        public Type[] Skills { get; set; }
        public bool CanDrive(Type type)
        {
            foreach(var typeHave in Skills)
            {
                if (type == typeHave)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Busy { get; set; }
        public DateTime[] StartTime { get; set; }
        public DateTime[] EndtTime { get; set; }
        public int numTrip { get; set; }
    }
}
