using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ParkX3
{
    public partial class AddCar : ContentPage
    {
        private HttpClient client;

        public AddCar(string marca, string modello)
        {
            InitializeComponent();
            brand.Text = marca;
            if (marca != "")
            {
                brand.IsEnabled = false;
            }
        }

        async void cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void save(object sender, EventArgs e)
        {
            var go = 0;

            if (string.IsNullOrWhiteSpace(status.Text))
            {
                status.Text = "0";
                go = 1;
            }
            if (status.Text == "0" || status.Text == "1" || status.Text == "2")
            {
                go = 1;
            }
            else
            {
                await DisplayAlert("Alert", "Wrong status. 0 = for sale, 1 = heritage, 2 = prototype", "OK");
                go = 0;
            }

            string url = "http://www.stritwalk.com/Park/api/" +
                "?action=uploadCar" +
                "&marca=" + brand.Text +
                "&modello=" + model.Text +
                "&vecchio=" + status.Text +
                "&motori=" + engines.Text +
                "&serieNuova=" + year.Text +
                "&serieVecchia=" + "" +
                "&immagine=" + image.Text +
                "&dimensioni=" + dimensions.Text +
                "&modelli=" + types.Text +
                "&prezzo=" + price.Text +
                "&descrizione=" + description.Text
                ;

            if (!string.IsNullOrWhiteSpace(brand.Text) && !string.IsNullOrWhiteSpace(model.Text))
            {
                if (go == 1)
                {
                    try
                    {
                        url = "http://www.stritwalk.com/Park/api/" +
                "?action=uploadCar" +
                "&marca=" + brand.Text +
                "&modello=" + model.Text +
                "&vecchio=" + status.Text +
                "&motori=" + engines.Text +
                "&serieNuova=" + year.Text +
                "&serieVecchia=" + "" +
                "&immagine=" + image.Text +
                "&dimensioni=" + dimensions.Text +
                "&modelli=" + types.Text +
                "&prezzo=" + price.Text +
                "&descrizione=" + description.Text
                ;
                        Car it = new Car();
                        it.marca = brand.Text;
                        it.modello = model.Text;
                        it.vecchio = status.Text;
                        it.action = "uploadCar";
                        url = "http://www.stritwalk.com/Park/api/";
                        var item = @"{""action"" : ""uploadCar"", ""marca"" : " + brand.Text + ", \"modello\" : " + model.Text + ", \"vecchi\" : " + status.Text + "}";
                        var json = JsonConvert.SerializeObject(it);
                        await DisplayAlert("Alert", json, "OK");
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = null;
                        client = new HttpClient();
                        client.MaxResponseContentBufferSize = 256000;
                        var uri = new Uri("http://www.stritwalk.com/Park/api/");
                        response = await client.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {                           
                            await Navigation.PopAsync();

                        }else
                        {
                            await DisplayAlert("Alert", response.ToString(), "OK");
                        }
                        /*
                        // Create an HTTP web request using the URL:
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Method = "POST";

                        // Send the request to the server and wait for the response:
                        using (WebResponse response = await request.GetResponseAsync())
                        {
                            // Get a stream representation of the HTTP web response:
                            using (Stream stream = response.GetResponseStream())
                            {
                                using (StreamReader reader = new StreamReader(stream))
                                {
                                    string content = reader.ReadToEnd();
                                    
                                }
                            }
                        }
                        */
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alert", ex.ToString(), "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Alert", "Brand and Model can not be empty", "OK");
            }

        }
    }
}
