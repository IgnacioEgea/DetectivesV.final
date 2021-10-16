using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public bool isAlive;

    public bool onLight;
    [Space]
    public bool vulnerableLuz; 

    [Space]
    [Header("Variables de control")]
    public bool finishDialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BossLight>())
        {
            onLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BossLight>())
        {
            onLight = false;
            vulnerableLuz = true;
        }
    }
}
