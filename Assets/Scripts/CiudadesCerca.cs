using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiudadesCerca : MonoBehaviour
{
    public string[] ciudades = { "Buga", "San Pedro", "Palmira", "Andalucía", "Bugalagrande" };
    private GameObject CrearCiudad(string nombreCiudad)
    {
        GameObject ciudad = new GameObject(nombreCiudad);
        ciudad.transform.localPosition = new Vector3(0, 0, 0);
        ciudad.transform.localRotation = Quaternion.identity;
        ciudad.transform.localScale = new Vector3(1, 1, 1);
        TextMesh textMesh = ciudad.AddComponent<TextMesh>();
        textMesh.text = nombreCiudad;
        return ciudad;
    }
    public void CreateAllCountries()
    {
        foreach (string nombreCiudad in ciudades)
        {
            GameObject ciudad = CrearCiudad(nombreCiudad);
            DontDestroyOnLoad(ciudad);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateAllCountries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
