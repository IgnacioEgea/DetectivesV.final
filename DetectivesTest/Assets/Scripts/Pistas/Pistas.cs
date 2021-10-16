using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistas : MonoBehaviour
{
    public GameObject Huellas;
    
    void Start()
    {
            
    }

    private void OnTriggerEnter(Collider other)
    {
        Huellas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Huellas.SetActive(false);
    }
}
