using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ObtenerCoordenadasUbicacion();
        }

        /*
         * Este codigo obtiene las coordenadas del dispositivo que lo ejecuta haciendo uso de la API de google, queda prueba pendiente 
         * mejorar accuracy pues muestra ubicación en Cali
        */
        private static async Task ObtenerCoordenadasUbicacion()
        {
            string apiKey = "AIzaSyBsdP6Cy3_K3ebNLJv_bRXw14zmvjP-C8g"; // 

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
                    Console.WriteLine(responseContent); // 

                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                    string lat = responseObject.location.lat;
                    string lng = responseObject.location.lng;

                    await ObtenerCiudadXCoordenadas(lat, lng, apiKey);

                }
                else
                {
                    Console.WriteLine("Errrrror al realizar la solicitud.");
                }
            }
        }
        /*
         * Este método obtiene el nombre de la ciudad según las coordenadas que se le pasen como parametros 
        */
        private static async Task ObtenerCiudadXCoordenadas(string lat, string lng, string apiClave)
        {
            string apiKey = apiClave;
            string latitude = lat;
            string longitude = lng;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
                client.DefaultRequestHeaders.Accept.Clear();

                string requestUrl = $"json?latlng={latitude},{longitude}&key={apiKey}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();


                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);



                    string formattedAddress = responseObject.results[0].formatted_address;
                    Console.WriteLine("Ciudad" + formattedAddress);
                }
                else
                {
                    Console.WriteLine("Error al realizar la solicitud.");
                }
            }
        }
    }
}
