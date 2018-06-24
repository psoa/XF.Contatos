using System;
using System.Collections.Generic;
using Plugin.MapsPlugin;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using XF.Contatos.API;

namespace XF.Contatos
{
    public partial class ContactView : ContentPage
    {
        public ContactView()
        {
            InitializeComponent();
        }

        private void btnCoordenada_Clicked(object sender, EventArgs e)
        {
            ILocalizacao geolocation = DependencyService.Get<ILocalizacao>();
            geolocation.GetCoordenada();

            MessagingCenter.Subscribe<ILocalizacao, Coordenada>
                (this, "coordenada", (objeto, geo) =>
                {
                    lblLongitude.Text = geo.Longitude;
                    lblLatitude.Text = geo.Latitude;

                });
        }


        private async void btnCamera_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                PhotoImage.Source = ImageSource.FromStream(() => { 
                return photo.GetStream(); 
            });
            
        }

        private void btnMapa_Clicked(object sender, EventArgs e)
        {
            ILocalizacao geolocation = DependencyService.Get<ILocalizacao>();
            geolocation.GetCoordenada();

            MessagingCenter.Subscribe<ILocalizacao, Coordenada>
            (this, "coordenada_map", (objeto, geo) =>
                {
                CrossMapsPlugin.Current.PinTo("Contato",
                                              Convert.ToDouble(geo.Latitude), 
                                              Convert.ToDouble(geo.Longitude), 8);  
                });


        }
    }
}
