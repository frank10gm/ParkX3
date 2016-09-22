using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace ParkX3
{
	public partial class CarsPage : ContentPage
	{
		public Marca marca;
		private ObservableCollection<GroupedCar> grouped { get; set; }
		public ObservableCollection<Car> jsonParsed;

		public CarsPage(Marca select)
		{
			InitializeComponent();
			this.Title = select.marca;
			marca = select;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await LoadCars();
		}

		void onSearch(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(e.NewTextValue))
			{
				CarsView.IsGroupingEnabled = true;
				CarsView.ItemTemplate.SetBinding(TextCell.TextProperty, "modello");
				CarsView.ItemsSource = grouped;
			}
			else
			{
				CarsView.IsGroupingEnabled = false;
				CarsView.ItemTemplate.SetBinding(TextCell.TextProperty, "modello");
				CarsView.ItemsSource = jsonParsed.Where(i => i.modello.ToLower().Contains(e.NewTextValue.ToLower()));
			}
		}

		async void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

			((ListView)sender).SelectedItem = null;
			var select = (Car)e.SelectedItem;

			await Navigation.PushAsync(new CarPage(select, marca.marca));
            searchBar.Text = "";
        }

		public async Task LoadCars()
		{
			if (jsonParsed == null)
			{
				loading.IsVisible = true;
				loading.IsRunning = true;
			}
			string url = "http://www.stritwalk.com/Park/api/" +
				"?action=getCars&marca="
				+ marca.marca;
			jsonParsed = await FetchCarsAsync(url);

			grouped = new ObservableCollection<GroupedCar>();
			var saleGroup = new GroupedCar() { Nome = "For sale" };
			var heritageGroup = new GroupedCar() { Nome = "Heritage" };
			var prototypeGroup = new GroupedCar() { Nome = "Prototype" };

			foreach (Car element in jsonParsed)
			{
				switch (element.vecchio)
				{
					case "0":
						saleGroup.Add(element);
						break;
					case "1":
						heritageGroup.Add(element);
						break;
					case "2":
						prototypeGroup.Add(element);
						break;
				}
			}

			if (saleGroup.Count > 0) grouped.Add(saleGroup);
			if (heritageGroup.Count > 0) grouped.Add(heritageGroup);
			if (prototypeGroup.Count > 0) grouped.Add(prototypeGroup);

			CarsView.ItemsSource = grouped;
		}

		private async Task<ObservableCollection<Car>> FetchCarsAsync(string url)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
			request.ContentType = "application/json";
			request.Method = "GET";

			// Send the request to the server and wait for the response:
			using (WebResponse response = await request.GetResponseAsync())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream())
				{

					using (StreamReader reader = new StreamReader(stream))
					{

						string content = reader.ReadToEnd();
						Debug.WriteLine(content);

						var result = await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<Car>>(content));

						loading.IsVisible = false;
						loading.IsRunning = false;

						return result;

					}
				}
			}
		}

	}
}
