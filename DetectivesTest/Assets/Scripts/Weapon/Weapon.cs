using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 10f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    private float NextTimeToFire = 0f;
    public Camera mainCamera;
    //public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public ThirdPersonCharacter tpc;
    public LayerMask Enemy;
    public BossIA bossIA;
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.deltaTime + 1f / fireRate;
            Shoot();
            //Physics.IgnoreCollision(tpc.GetComponent<CapsuleCollider>(), GetComponent<CapsuleCollider>());
        }
    }

    void Shoot()
    {
       
        //Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)

        //muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range,Enemy))
        {
          
            Debug.Log(hit.transform.name);
            GameObject boss = hit.transform.gameObject;
            BossHPManager target = boss.transform.GetComponent<BossHPManager>();

            if (target != null)
            {
                if (bossIA.Stunned == true)
                {
                 target.BossReceivedDamage(damage);
                }
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce );
            } 

            GameObject impactGO = Instantiate(impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2);
        }
    }
}
