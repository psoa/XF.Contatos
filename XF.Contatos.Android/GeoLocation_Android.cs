using Android.App;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Geolocation;
using XF.Contatos.API;
using XF.Contatos.Droid;

[assembly: Dependency(typeof(GeoLocation_Android))]
namespace XF.Contatos.Droid
{
    public class GeoLocation_Android : ILocalizacao
    {
        public void GetCoordenada()
        {
            var context = MainApplication.CurrentContext as Activity;
            var locator = new Geolocator(context) { DesiredAccuracy = 50 };

            locator.GetPositionAsync(timeout: 10000).ContinueWith(t => {
                SetCoordenada(t.Result.Latitude, t.Result.Longitude);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void SetCoordenada(double paramLatitude, double paramLongitude)
        {
            var coordenada = new Coordenada()
            {
                Latitude = paramLatitude.ToString(),
                Longitude = paramLongitude.ToString()
            };

            // Enviar coordenada via MessagingCenter
            MessagingCenter.Send<ILocalizacao, Coordenada>
                (this, "coordenada", coordenada);
            MessagingCenter.Send<ILocalizacao, Coordenada>
                (this, "coordenada_map", coordenada);
        }
    }
}
