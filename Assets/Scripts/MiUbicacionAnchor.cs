using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;

public class MiUbicacionAnchor : MonoBehaviour
{
    private ARGeospatialCreatorAnchor anchor;
    // Start is called before the first frame update
    void Start()
    {
        anchor = GetComponent<ARGeospatialCreatorAnchor>();
    }

    // Update is called once per frame
    void Update()
    {
        anchor.Latitude = 4.0782752;
        anchor.Longitude = -76.186974;
        anchor.Altitude = 1012;
    }
}
