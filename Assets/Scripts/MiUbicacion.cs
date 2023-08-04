using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CesiumForUnity;

public class MiUbicacion : MonoBehaviour
{
    private CesiumGeoreference miUbicacion;
    // Start is called before the first frame update
    void Start()
    {
        miUbicacion = GetComponent<CesiumGeoreference>();
    }

    // Update is called once per frame
    void Update()
    {
        miUbicacion.SetOriginLongitudeLatitudeHeight(-76.237481, 4.0910646, 1100);
        Debug.Log("Mi ubicación es: " + miUbicacion.longitude + miUbicacion.latitude);
    }
}
