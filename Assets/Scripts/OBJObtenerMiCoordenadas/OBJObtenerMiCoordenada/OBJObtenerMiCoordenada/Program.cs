using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OBJObtenerMiCoordenada
{
    class Posicion
    {
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }

    class Geolocalizador
    {
        string apiKey = "AIzaSyBsdP6Cy3_K3ebNLJv_bRXw14zmvjP-C8g"; // 
        public async Task<Posicion> ObtenerPosicion()
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.googleapis.com/geolocation/v1/");
                client.DefaultRequestHeaders.Accept.Clear();

                var requestContent = new StringContent("{\"considerIp\": \"true\"}");

                string requestUrl = $"geolocate?key={apiKey}";

                HttpResponseMessage response = await client.PostAsync(requestUrl, requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                    Posicion posicion = new Posicion();
                    posicion.Latitud = responseObject.location.lat;
                    posicion.Longitud = responseObject.location.lng;
                    return posicion;
                }
                else
                {
                    Console.WriteLine("Error al realizar la solicitud.");
                    return null;
                }
            }
        }

        public async Task<string> ObtenerNombreMiCiudad(string latitud, string longitud)
        {
            string lat = latitud;
            string lng = longitud;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
                client.DefaultRequestHeaders.Accept.Clear();

                string requestUrl = $"json?latlng={lat},{lng}&key={apiKey}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    dynamic respuestaJson = JsonConvert.DeserializeObject(responseContent);

                    string nombreCiudadEnFormato = respuestaJson.results[9].formatted_address;
                    int indexComa = nombreCiudadEnFormato.IndexOf(',');
                    string nombreMiCiudad = nombreCiudadEnFormato.Substring(0, indexComa);
                   
                    return nombreMiCiudad;
                }
                else
                {
                    Console.WriteLine("Error al realizar la solicitud.");
                    return null;
                }
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Geolocalizador geolocalizador = new Geolocalizador();
            Posicion posicion = await geolocalizador.ObtenerPosicion();
            string lat = posicion.Latitud;
            string lng = posicion.Longitud;
            Console.WriteLine("Coordenadas Lat: "+posicion.Latitud+" Long: "+posicion.Longitud);
            string nombreCiudad = await geolocalizador.ObtenerNombreMiCiudad(lat,lng);
            Console.WriteLine("Ciudad: " + nombreCiudad);
        }
    }


}
