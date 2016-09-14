using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Diagnostics;

namespace ParkX3
{
	public partial class CarsPage : ContentPage
	{

		public Marca marca;

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

		async void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

			((ListView)sender).SelectedItem = null;
			var select = (Car)e.SelectedItem;

			//await Navigation.PushAsync(new CarPage(select));
		}

		public async Task LoadCars()
		{
			string url = "http://www.stritwalk.com/Park/api/" +
				"?action=getCars&marca="
				+ marca.marca;
			var jsonParsed = await FetchCarsAsync(url);
			CarsView.ItemsSource = jsonParsed;
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
						// Return the JSON document:

						return result;

					}
				}
			}
		}

	}
}
