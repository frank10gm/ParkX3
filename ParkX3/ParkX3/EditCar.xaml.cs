using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ParkX3
{
    public partial class EditCar : ContentPage
    {
        int del = 0;

        public EditCar(string marca, string modello)
        {
            InitializeComponent();
            brand.Text = marca;
            model.Text = modello;
            if (marca != "")
            {
                brand.IsEnabled = false;
            }
            if(modello != "")
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
            await Navigation.PopAsync();
        }

        async void delete(object sender, EventArgs e)
        {
            if(del == 0)
            {
                pass.IsVisible = true;
                passL.IsVisible = true;
                del = 1;
            }else if(del == 1)
            {
                if (pass.Text != "deletami")
                    await DisplayAlert("Alert", "Wrong password", "OK");
                else
                {

                    await Navigation.PopAsync();
                }

            }
        }
    }
}
