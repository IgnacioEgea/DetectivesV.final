using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPManager : MonoBehaviour
{
    public Image HPFill;
    public float maxHealth;
    float healthAux;
    public CheckpointManager CheckPointScript;
    public GameObject Player;
    void Start()
    {
        healthAux = maxHealth;
    }

    public void PlayerReceivedDamage(float damage)
    {
        maxHealth -= damage;
        HPFill.fillAmount = maxHealth / healthAux;

        if (maxHealth <= 0)
        {
            Player.transform.position = CheckPointScript.currentLocationToSpawn.position;
            maxHealth = 100;
            HPFill.fillAmount = maxHealth / healthAux;
        }
    }

}
