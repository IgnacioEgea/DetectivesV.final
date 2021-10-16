using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista5 : MonoBehaviour
{

    public GameObject Sangre;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Sangre.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Sangre.SetActive(false);
    }
}
