using Foundation;
using UIKit;
using Xamarin.Forms;
using XF.Contatos.API;
using XF.Contatos.iOS;

[assembly: Dependency(typeof(Ligar_iOS))]
namespace XF.Contatos.iOS
{
    public class Ligar_iOS : ILigar
    {
        public bool Discar(string telefone)
        {
            return UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + telefone));
        }
    }
}
