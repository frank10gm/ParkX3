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
                        using (var client = new HttpClient())
                        {
                            var values = new Dictionary<string, string>
                            {
                                { "action", "uploadCar" },
                                { "marca", brand.Text },
                                { "modello", model.Text },
                                { "vecchio", status.Text },
                                { "motori", engines.Text },
                                {"serieNuova", year.Text },
                                {"serieVecchia", "" },
                                {"immagine", image.Text },
                                {"dimensioni", dimensions.Text },
                                {"modelli", types.Text },
                                {"prezzo", price.Text },
                                {"descrizione", description.Text }

                            };

                            var content = new FormUrlEncodedContent(values);
                            var response = await client.PostAsync("http://www.stritwalk.com/Park/api/", content);
                            var responseString = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                await Navigation.PopAsync();
                            }
                        }
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
