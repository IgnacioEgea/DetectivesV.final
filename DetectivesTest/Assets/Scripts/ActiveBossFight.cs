using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossFight : MonoBehaviour
{

    public BossIA bossIA;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bossIA.CambiarGeneralEstado(EnumBoss.BossIA.BossGeneralStates.Battle);
            Debug.Log("Yes");
        }
    }

}
