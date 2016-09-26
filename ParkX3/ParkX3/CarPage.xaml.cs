using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ParkX3
{
    public partial class CarPage : ContentPage
    {
        public string marca_g;
        public string modello_g;
        Car selezione;
        public Car select;
        private string marca_s;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loadLayout(marca_s);
        }

        public CarPage(Car selection, string marca)
        {
            InitializeComponent();
            select = selection;
            marca_s = marca;
            //loadLayout(marca_s);
        }

        void loadLayout(string marca)
        {
            lay.Children.Clear();
            Title = select.modello;
            marca_g = marca;
            modello_g = select.modello;
            selezione = select;
            modello.Text = marca + " " + select.modello + Environment.NewLine;
            if (select.serieNuova != "")
                breveDesc.Text = "Year: " + select.serieNuova;

            FormattedString fs = new FormattedString();
            fs.Spans.Add(new Span
            {
                Text = marca + " " + select.modello,
                FontSize = 16
            });
            fs.Spans.Add(new Span
            {
                Text = Environment.NewLine + Environment.NewLine,
                FontSize = 6
            });
            if (select.serieNuova != "")
                fs.Spans.Add(new Span
                {
                    Text = "Year: " + select.serieNuova,
                    FontSize = 14
                });
            Frame top = new Frame
            {
                Content = new Label
                {
                    FormattedText = fs
                },
                HasShadow = true,
                Margin = new Thickness(20, 12, 20, 3)
            };

            fs = new FormattedString();
            fs.Spans.Add(new Span
            {
                Text = "Year",
                FontSize = 16
            });
            fs.Spans.Add(new Span
            {
                Text = Environment.NewLine + Environment.NewLine,
                FontSize = 6
            });
            fs.Spans.Add(new Span
            {
                Text = select.serieNuova,
                FontSize = 14
            });
            Frame year = new Frame
            {
                Content = new Label
                {
                    FormattedText = fs
                },
                HasShadow = true,
                Margin = new Thickness(20, 3, 20, 3)
            };

            fs = new FormattedString();
            fs.Spans.Add(new Span
            {
                Text = "Engines",
                FontSize = 16
            });
            fs.Spans.Add(new Span
            {
                Text = " " + Environment.NewLine + Environment.NewLine,
                FontSize = 6
            });
            fs.Spans.Add(new Span
            {
                Text = select.motori,
                FontSize = 14
            });
            Frame engines = new Frame
            {
                Content = new Label
                {
                    FormattedText = fs
                },
                HasShadow = true,
                Margin = new Thickness(20, 3, 20, 3)
            };

            fs = new FormattedString();
            fs.Spans.Add(new Span
            {
                Text = "Types",
                FontSize = 16
            });
            fs.Spans.Add(new Span
            {
                Text = Environment.NewLine + Environment.NewLine,
                FontSize = 6
            });
            fs.Spans.Add(new Span
            {
                Text = select.modelli,
                FontSize = 14
            });
            Frame types = new Frame
            {
                Content = new Label
                {
                    FormattedText = fs
                },
                HasShadow = true,
                Margin = new Thickness(20, 3, 20, 3)
            };

            fs = new FormattedString();
            fs.Spans.Add(new Span
            {
                Text = "Dimensions",
                FontSize = 16
            });
            fs.Spans.Add(new Span
            {
                Text = Environment.NewLine + Environment.NewLine,
                FontSize = 6
            });
            fs.Spans.Add(new Span
            {
                Text = select.dimensioni,
                FontSize = 14
            });
            Frame dimensions = new Frame
            {
                Content = new Label
                {
                    FormattedText = fs
                },
                HasShadow = true,
                Margin = new Thickness(20, 3, 20, 3)
            };

            fs = new FormattedString();
            fs.Spans.Add(new Span
            {
                Text = "Price",
                FontSize = 16
            });
            fs.Spans.Add(new Span
            {
                Text = Environment.NewLine + Environment.NewLine,
                FontSize = 6
            });
            fs.Spans.Add(new Span
            {
                Text = select.prezzo,
                FontSize = 14
            });
            Frame price = new Frame
            {
                Content = new Label
                {
                    FormattedText = fs
                },
                HasShadow = true,
                Margin = new Thickness(20, 3, 20, 3)
            };

            fs = new FormattedString();
            fs.Spans.Add(new Span
            {
                Text = "Description",
                FontSize = 16
            });
            fs.Spans.Add(new Span
            {
                Text = Environment.NewLine + Environment.NewLine,
                FontSize = 6
            });
            fs.Spans.Add(new Span
            {
                Text = select.descrizione,
                FontSize = 14
            });
            Frame description = new Frame
            {
                Content = new Label
                {
                    FormattedText = fs
                },
                HasShadow = true,
                Margin = new Thickness(20, 3, 20, 3)
            };

            lay.Children.Add(top);
            if (select.motori != "")
                lay.Children.Add(engines);
            if (select.modelli != "")
                lay.Children.Add(types);
            if (select.dimensioni != "")
                lay.Children.Add(dimensions);
            if (select.prezzo != "")
                lay.Children.Add(price);
            if (select.descrizione != "")
                lay.Children.Add(description);

            try
            {
                Frame image = new Frame
                {
                    Content = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        Source = new UriImageSource
                        {
                            Uri = new Uri(select.immagine),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(5, 0, 0, 0)
                        }
                    },
                    Margin = new Thickness(20, 3, 20, 12)
                };
                lay.Children.Add(image);
            }
            catch
            {

            }

            Button edit = new Button
            {
                Text = "Edit",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 6)
            };
            //lay.Children.Add(edit);
        }

        async void editCar(object sender, EventArgs e)
        {
            EditCar edit = new EditCar(marca_g, modello_g, selezione.vecchio, selezione.motori, selezione.serieNuova, "", selezione.immagine, selezione.dimensioni, selezione.modelli, selezione.prezzo, selezione.descrizione, selezione.id);
            edit.EditSuccess += async (sender2, eventArgs) =>
            {
                if (eventArgs.Args[0].ToString() == "del")
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    select.vecchio = eventArgs.Args[0].ToString();
                    select.motori = eventArgs.Args[1].ToString();
                    select.serieNuova = eventArgs.Args[2].ToString();
                    select.immagine = eventArgs.Args[3].ToString();
                    select.dimensioni = eventArgs.Args[4].ToString();
                    select.modelli = eventArgs.Args[5].ToString();
                    select.prezzo = eventArgs.Args[6].ToString();
                    select.descrizione = eventArgs.Args[7].ToString();
                    loadLayout(marca_s);
                }
            };
            await Navigation.PushAsync(edit);
        }

    }
}
