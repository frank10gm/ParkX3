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
        }
    }
}
