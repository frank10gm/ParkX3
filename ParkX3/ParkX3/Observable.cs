using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkX3
{
    class Observable
    {
        public event EventHandler<Car> SomethingHappened;

        public void DoSomething(Car car)
        {
         
        }
    }
}
