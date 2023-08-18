using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CiudadesCerca : MonoBehaviour
{
    [HideInInspector]
    public string apiKey = "AIzaSyBsdP6Cy3_K3ebNLJv_bRXw14zmvjP-C8g";

    public double latitude = 4.0782752; // Aquí ingresas las coordenadas de tu ubicación
    public double longitude = -76.186974;

    public float maxDistance = 100000; // Distancia máxima en metros (100 km = 100,000 m)

    private const string googlePlacesURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
    public string[] ciudades;

    private IEnumerator GetNearbyCities()
    {
        string url = $"{googlePlacesURL}?location={latitude},{longitude}&radius={maxDistance}&key={apiKey}";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al conectarse a Google Places API: " + www.error);
            }
            else
            {
                // Procesar la respuesta JSON y obtener los nombres de las ciudades
                string jsonResult = www.downloadHandler.text;
                ParseCityNames(jsonResult);
            }
        }
    }

    private void ParseCityNames(string json)
    {
        
        // Aquí debes parsear el JSON recibido y extraer los nombres de las ciudades
        // Puedes usar bibliotecas como MiniJSON o JSONUtility para realizar el parsing.
        // Asumiremos que el JSON tiene un campo llamado "name" para el nombre de la ciudad.

    // Ejemplo de cómo se podría hacer el parsing con JSONUtility (requiere estructura adecuada del JSON):
    CityData cityData = JsonUtility.FromJson<CityData>(json);

        if (cityData != null && cityData.results != null)
        {
            int i = 0;
            foreach (Result result in cityData.results)
            {
                ciudades[i] = result.name;
                Debug.Log("City: " + result.name);
                i++;
            }
        }
    }
 
    private GameObject CrearCiudad(string nombreCiudad, int distanciaX)
    {
        GameObject ciudad = new GameObject(nombreCiudad);
        ciudad.transform.localPosition = new Vector3(distanciaX, 0, 0);
        ciudad.transform.localRotation = Quaternion.identity;
        ciudad.transform.localScale = new Vector3(1, 1, 1);
        TextMesh textMesh = ciudad.AddComponent<TextMesh>();
        textMesh.text = nombreCiudad;
        return ciudad;
    }
    public void CreateAllCountries()
    {
        int distancia = 0;
        foreach (string nombreCiudad in ciudades)
        {
            distancia++;
            GameObject ciudad = CrearCiudad(nombreCiudad, distancia);
            DontDestroyOnLoad(ciudad);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetNearbyCities());
        CreateAllCountries();
        Debug.Log(ciudades);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class CityData
{
    public Result[] results;
}

[System.Serializable]
public class Result
{
    public string name;
    // Otros campos del resultado que puedas necesitar
}