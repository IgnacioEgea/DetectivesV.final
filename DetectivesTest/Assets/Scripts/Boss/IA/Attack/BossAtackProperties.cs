using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtackProperties : MonoBehaviour
{
    public float damageProyectil;

    public void AutoDestruccion(float TiempoDestruccionProyectil)
    {
        Destroy(this.gameObject, TiempoDestruccionProyectil);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Dañar al jugador
            PlayerHPManager hpScript = other.GetComponent<PlayerHPManager>();
            hpScript.PlayerReceivedDamage(damageProyectil);
        }
    }
}
