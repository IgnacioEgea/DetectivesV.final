using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public GameObject healthPack;
    public int LifePack = 2;
    public GameObject tmProLifes;
    //public PlayerHealth playerHealth;
    //public Movement movementScript;
    Animator cmpAnimator;
    public GameObject player;
    float timerReloadReset;
    float timerReload = 5.567f;
    bool activateAnimation = false;
    void Start()
    {
        cmpAnimator = player.GetComponent<Animator>();
        timerReloadReset = timerReload;
        //movementScript = player.GetComponent<Movement>();
    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        tmProLifes.SetActive(true);
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            cmpAnimator.SetTrigger("LiftObject");
            tmProLifes.SetActive(false);
            healthPack.SetActive(false);
            activateAnimation = true;
            //Inmobilize();
            //if(playerHealth.playerLives <= 3)
            //{
            //    playerHealth.playerLives++;
            //}
        }
        if (activateAnimation == true)
        {
            timerReload -= Time.deltaTime;
        }
        if (timerReload <= 0)
        {
            timerReload = timerReloadReset;
            cmpAnimator.ResetTrigger("LiftObject");
            activateAnimation = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        tmProLifes.SetActive(false);
    }
}
//    void Inmobilize()
//    {
//        movementScript.transitionObjectActive = true;
//    }
//}
