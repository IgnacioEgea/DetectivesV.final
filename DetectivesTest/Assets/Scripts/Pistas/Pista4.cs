using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista4 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PeloNegro;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PeloNegro.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        PeloNegro.SetActive(false);
    }
}
