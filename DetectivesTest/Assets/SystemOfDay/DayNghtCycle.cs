using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNghtCycle : MonoBehaviour
{
    public float Timer;
    public float maxTimer;
    public GameObject directionalLight;
    public float minAngle;
    public float maxAngle;
    public float currentAngle;
    public GameObject UI;

    float aux;


    void Start()
    {
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(minAngle, 0, 0));
        Timer = maxTimer;
        currentAngle = minAngle;
        aux = maxAngle - minAngle;
    }

    void Update()
    {
        if (Timer >= 0)
        {
            directionalLight.transform.rotation = Quaternion.Euler(new Vector3(currentAngle, 0, 0));
            currentAngle += Time.deltaTime * (aux / maxTimer);
            Timer -= Time.deltaTime;
            UI.transform.Rotate(new Vector3(0, 0, 180 / maxTimer * Time.deltaTime));
        }
    }
}
