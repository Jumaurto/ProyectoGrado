using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CesiumForUnity;


public class MiUbicacion : MonoBehaviour
{
    private CesiumGeoreference miUbicacion;
    public LocationService _locationService;
    float latitud;
    float longitud;
    float altitud;

    // Start is called before the first frame update
    void Start()
    {
        miUbicacion = GetComponent<CesiumGeoreference>();
        _locationService.Start();

    }

    // Update is called once per frame
    void Update()
    {
        latitud = Input.location.lastData.latitude;
        longitud = Input.location.lastData.longitude;
        altitud = Input.location.lastData.altitude;
        Debug.Log("My position is:" + latitud + " " + longitud + " " + altitud);
        miUbicacion.SetOriginLongitudeLatitudeHeight(longitud, latitud, altitud);
    }
}
