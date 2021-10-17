using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLight : MonoBehaviour
{
    public GameObject SpawnLight;

    private void Start()
    {
        SpawnLight.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        SpawnLight.SetActive(true);
    }
}
