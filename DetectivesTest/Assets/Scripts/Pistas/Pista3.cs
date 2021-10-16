using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista3 : MonoBehaviour
{
    public GameObject Arañazos;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Arañazos.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Arañazos.SetActive(false);
    }
}
