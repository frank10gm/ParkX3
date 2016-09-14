using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;


namespace ParkX3
{
	public partial class MarchePage : ContentPage
	{

		public MarchePage()
		{
			InitializeComponent();
			NavigationPage.SetTitleIcon(this, "icon-76.png");
			//this.Title = "Park";
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await LoadMarche();
		}

		void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}
			DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
			//((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
		}

		public async Task LoadMarche()
		{
			string url = "http://www.stritwalk.com/Park/api/" +
				"?action=getMarche";
			var jsonParsed = await FetchMarcheAsync(url);
			MarcheView.ItemsSource = jsonParsed;
		}

		public class Marca
		{
			public string marca { get; set; }
			public string logo;
			public string descrizione;
		}

		private async Task<ObservableCollection<Marca>> FetchMarcheAsync(string url)
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
						//Debug.WriteLine(content);

						var result = await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<Marca>>(content));
						// Return the JSON document:

						return result;
					
					}
				}
			}
		}

	}
}
