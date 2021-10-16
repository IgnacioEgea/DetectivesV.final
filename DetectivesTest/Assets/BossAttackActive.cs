using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackActive : MonoBehaviour
{

    public float damageBoss;
    public float PlayerInvulnerability;
    float PlayerInvulnerabilityReset;
    bool Vulnerable;
    BoxCollider staffCollider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Vulnerable == true)
        {
            staffCollider.enabled = false;
            Vulnerable = false;
            PlayerHPManager PlayerHPScript = other.GetComponent<PlayerHPManager>();
            PlayerHPScript.PlayerReceivedDamage(damageBoss);
        }
    }

    void Start()
    {
        staffCollider = this.GetComponent<BoxCollider>();
        Vulnerable = true;
        PlayerInvulnerabilityReset = PlayerInvulnerability;
    }

    
    void Update()
    {
        if (Vulnerable == false )
        {
            PlayerInvulnerability -= Time.deltaTime;
            if (PlayerInvulnerability <= 0)
            {
                staffCollider.enabled = true;
                PlayerInvulnerability = PlayerInvulnerabilityReset;
                Vulnerable = true;
            }
        }
    }
}
