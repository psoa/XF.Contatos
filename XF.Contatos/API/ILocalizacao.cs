using System;
namespace XF.Contatos.API
{
    public interface ILocalizacao
    {
        void GetCoordenada();
    }

    public class Coordenada
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
