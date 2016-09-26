using System;
using System.Collections.ObjectModel;

namespace ParkX3
{
    public class Car : EventArgs
    {
      
        public string action { get; set; }
        public string marca { get; set; }
        public string modello { get; set; }
        public string vecchio { get; set; }
        public string breveDesc { get; set; }
        public string descrizione { get; set; }
        public string immagine { get; set; }
        public string dimensioni { get; set; }
        public string motori { get; set; }
        public string modelli { get; set; }
        public string serieNuova { get; set; }
        public string serieVecchia { get; set; }
        public string prezzo { get; set; }
        public string immagineVecchie { get; set; }
        public string id { get; set; }
        public string detail
        {
            get
            {
                if (motori != "")
                    return serieNuova + " - " + motori;
                else
                    return serieNuova;
            }
        }

        public object[] Args { get; set; }

        public Car(params object[] args)
        {
            Args = args;
        }
    }

    public class GroupedCar : ObservableCollection<Car>
    {
        public string Nome { get; set; }
    }
}
