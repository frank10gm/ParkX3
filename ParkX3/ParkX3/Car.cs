using System;
using System.Collections.ObjectModel;

namespace ParkX3
{
	public class Car
	{
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

		public Car()
		{
		}
	}

	public class GroupedCar : ObservableCollection<Car>
	{
		public string Nome { get; set; }
	}
}
