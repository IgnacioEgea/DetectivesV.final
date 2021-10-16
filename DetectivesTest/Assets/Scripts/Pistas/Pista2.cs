using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista2 : MonoBehaviour
{
    public GameObject Cadaver;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Cadaver.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Cadaver.SetActive(false);
    }
}
