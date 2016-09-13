using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkX3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void onSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            valueLabel.Text = args.NewValue.ToString();
            
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            //Button button = (Button)sender;
            /*await DisplayAlert("Clicked",
                "The button labeled '" + valueLabel.Text + "' has been clicked.",
                "OK");*/

            //Navigation.PushAsync(new About("tua mamma"));
			await Navigation.PushAsync(new MarchePage());
        }
    }
}
