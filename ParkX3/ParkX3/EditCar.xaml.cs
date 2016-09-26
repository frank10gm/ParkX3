using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ParkX3
{
    public partial class EditCar : ContentPage
    {
        int del = 0;
        private string id_s;
        public event EventHandler<Car> EditSuccess;

        public EditCar(string marca, string modello, string vecchio, string motori, string serieNuova, string serieVecchia, string immagine, string dimensioni, string modelli, string prezzo, string descrizione, string id)
        {
            InitializeComponent();
            brand.Text = marca;
            model.Text = modello;
            status.Text = vecchio;
            engines.Text = motori;
            year.Text = serieNuova;
            image.Text = immagine;
            dimensions.Text = dimensioni;
            types.Text = modelli;
            price.Text = prezzo;
            description.Text = descrizione;
            id_s = id;
            if (marca != "")
            {
                brand.IsEnabled = false;
            }
            if (modello != "")
            {
                model.IsEnabled = false;
            }
            pass.IsVisible = false;
            passL.IsVisible = false;
            del = 0;    
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
                                { "action", "editCar" },
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
                                {"descrizione", description.Text },
                                {"id", id_s }
                            };

                            var content = new FormUrlEncodedContent(values);
                            var response = await client.PostAsync("http://www.stritwalk.com/Park/api/", content);
                            var responseString = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                EditSuccess?.Invoke(this, new Car(status.Text, engines.Text, year.Text, image.Text, dimensions.Text, types.Text, price.Text, description.Text));
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

        async void delete(object sender, EventArgs e)
        {
            if (del == 0)
            {
                pass.IsVisible = true;
                passL.IsVisible = true;
                del = 1;
            }
            else if (del == 1)
            {
                if (pass.Text != "deletami")
                    await DisplayAlert("Alert", "Wrong password", "OK");
                else
                {
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            var values = new Dictionary<string, string>
                            {
                                { "action", "deleteCar" },
                                { "marca", brand.Text },
                                { "modello", model.Text },
                                {"id", id_s }
                            };

                            var content = new FormUrlEncodedContent(values);
                            var response = await client.PostAsync("http://www.stritwalk.com/Park/api/", content);
                            var responseString = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                EditSuccess?.Invoke(this, new Car("del"));
                                await Navigation.PopAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alert", ex.ToString(), "OK");
                    }
                    await Navigation.PopAsync();
                }

            }
        }
    }
}
