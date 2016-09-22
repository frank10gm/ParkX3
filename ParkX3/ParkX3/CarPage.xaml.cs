using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ParkX3
{
    public partial class CarPage : ContentPage
    {
        public CarPage(Car select, string marca)
        {
            InitializeComponent();
            this.Title = select.modello;
            modello.Text = marca + " " + select.modello + Environment.NewLine;
            breveDesc.Text = "Year: " + select.serieNuova;

            FormattedString fs = new FormattedString();
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

            if (select.motori != "")
                lay.Children.Add(engines);
            if (select.modelli != "")
                lay.Children.Add(types);
            if (select.dimensioni != "")
                lay.Children.Add(dimensions);
            if (select.prezzo != "")
                lay.Children.Add(price);

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
            lay.Children.Add(edit);
        }

    }
}
