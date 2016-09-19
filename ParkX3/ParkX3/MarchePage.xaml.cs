using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using System.Linq;


namespace ParkX3
{
	public partial class MarchePage : ContentPage
	{
		public ObservableCollection<Marca> jsonParsed;

		public MarchePage()
		{
			InitializeComponent();
            this.Title = "Park";
            NavigationPage.SetTitleIcon(this, "top.png");          
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await LoadMarche();
		}

		void onSearch(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(e.NewTextValue))
				MarcheView.ItemsSource = jsonParsed;
			else
				MarcheView.ItemsSource = jsonParsed.Where(i => i.marca.ToLower().Contains(e.NewTextValue.ToLower()));
		}

		async void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

			((ListView)sender).SelectedItem = null;
			var select = (Marca)e.SelectedItem;

			await Navigation.PushAsync(new CarsPage(select));
			searchBar.Text = "";
		}

		public async Task LoadMarche()
		{
			if (jsonParsed == null)
			{
				loading.IsVisible = true;
				loading.IsRunning = true;
			}

			string url = "http://www.stritwalk.com/Park/api/" +
				"?action=getMarche";
			jsonParsed = await FetchMarcheAsync(url);
			MarcheView.ItemsSource = jsonParsed;
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
						loading.IsVisible = false;
						loading.IsRunning = false;

						return result;

					}
				}
			}
		}

	}
}
