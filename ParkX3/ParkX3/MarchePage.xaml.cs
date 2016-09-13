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

		ObservableCollection<MarcheClass> marche = new ObservableCollection<MarcheClass>();

		public MarchePage()
		{
			InitializeComponent();

			MarcheView.ItemsSource = marche;

			Debug.WriteLine("start page");


			//marche.Add(new MarcheClass { DisplayName = "Bugatti" });
			//marche.Add(new MarcheClass { DisplayName = "Audi" });
		}


		protected async override void OnAppearing()
		{
			base.OnAppearing();
			System.Diagnostics.Debug.WriteLine("*****Here*****");
			await LoadMarche();
		}

		public async Task LoadMarche()
		{
			string url = "http://www.stritwalk.com/Park/api/" +
				"?action=getMarche";
			var jsonParsed = await FetchMarcheAsync(url);

			Debug.WriteLine(jsonParsed[1].marca);


			/*await DisplayAlert("Clicked",
				"The button labeled '" + json + "' has been clicked.",
				"OK");*/

		}

		public class Person
		{
			public string marca;
			public string logo;
			public string descrizione;
		}
		public class Record
		{
			public Person record;
		}

		private async Task<List<Person>> FetchMarcheAsync(string url)
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

						var result = await Task.Run(() => JsonConvert.DeserializeObject<List<Person>>(content));
						// Return the JSON document:

						return result;
					
					}
				}
			}
		}

	}
}
