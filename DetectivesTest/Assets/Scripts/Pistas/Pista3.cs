using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista3 : MonoBehaviour
{
    public GameObject Ara�azos;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Ara�azos.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Ara�azos.SetActive(false);
    }
}
