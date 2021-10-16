using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLight : MonoBehaviour
{
    public GameObject SpawnLight;

    public float Timer;
    public float ResetTimer;

    private void Start()
    {
        SpawnLight.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        SpawnLight.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (Timer <= 0)
        {
            Timer = ResetTimer;
        }
        Timer -= Time.deltaTime;
    }
}
