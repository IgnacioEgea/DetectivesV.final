using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPManager : MonoBehaviour
{
    BossIA cmpMovementIA;

    public Image HPFill;
    public float maxHealth;
    float healthAux;
    public GameObject PercentActive;
    public GameObject Disable;
    public GameObject ActiveGO;
    private void Awake()
    {
        cmpMovementIA = GetComponent<BossIA>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    BossReceivedDamage(10);
        //}

    }

    void Start()
    {
        healthAux = maxHealth;
        PercentActive.SetActive(false);
        Disable.SetActive(true);
        ActiveGO.SetActive(true);
    }

    public void BossReceivedDamage(float damage)
    {
        maxHealth -= damage;
        HPFill.fillAmount = maxHealth / healthAux;


        if (maxHealth <= healthAux * 0.25f)
        {
            cmpMovementIA.CambiarFaseBoss(EnumBoss.BossIA.BossStages.LowLife);
            PercentActive.SetActive(true);
            Disable.SetActive(false);
            ActiveGO.SetActive(false);

        }
        else if (maxHealth <= healthAux * 0.5f)
        {
            cmpMovementIA.CambiarFaseBoss(EnumBoss.BossIA.BossStages.HalfLife);
        }

        if (maxHealth == 0)
        {
            Destroy(this.gameObject);
        }
    }
}

