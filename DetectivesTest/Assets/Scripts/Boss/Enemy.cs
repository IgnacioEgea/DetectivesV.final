using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public Transform myTrans;
    public Transform target;
    public float speed = 1f;
    // Use this for initialization
    void Start()
    {
        //cache you public gameobject/transform
        myTrans = this.transform;

    }
   
    // Update is called once per frame
    void Update()
    {

        //interpolate from current position to the target over time
        myTrans.position = Vector3.Lerp(myTrans.position, target.position, speed * Time.deltaTime);
      
        if (myTrans.position == target.position)
        {
            speed = 0;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }


    }
    void Die()
    {
        Destroy(gameObject);
    }
   

}
