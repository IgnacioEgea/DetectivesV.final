using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonFire : MonoBehaviour
{
    public DayNghtCycle dayCicle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dayCicle.currentAngle = dayCicle.maxAngle;
        }
    }
}
