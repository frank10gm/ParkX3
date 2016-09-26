using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkX3
{
    class Observer
    {
        public void HandleEvent(object sender, EventArgs args)
        {
            Debug.WriteLine("Something happened to " + sender);
        }
    }
}
