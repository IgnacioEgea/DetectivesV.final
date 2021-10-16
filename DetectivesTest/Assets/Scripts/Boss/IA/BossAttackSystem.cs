using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSystem : MonoBehaviour
{
    BossIA cmpMovementIA;
    [Header("Referncias")]
    public GameObject prefabAtack;
    [Space]
    public Transform pivoteSpawnAtaque;
    [Space]
    /// Timers (son los que se cargan en time.deltatime)
    float timerToStartAtk;
    float timerToAbleToAtk;

    //variables de tiempo

    public EnumBoss.BossIA.EstadosAtaque estadoenemigoParaAtacar;

    public bool ejecutarAtaque;

    [Space]
    [Space]
    public float tiempoParaActivarAtaque;
    public float tiempoParaPoderVolverAAtacar;
    [Space]
    [Header("DatosProyectil")]
    public float damageAttack;
    public float TiempoDestrucciónProyectil;
    public bool padreBoss;
    
    private void Awake()
    {
        cmpMovementIA = GetComponent<BossIA>();

        CambiarEstadoAtaqueJugador(EnumBoss.BossIA.EstadosAtaque.ListoParaAtacar);

        //ObtenerPropiedadesDelAtaque();
    }

    private void Update()
    {
        switch (estadoenemigoParaAtacar)
        {
            case EnumBoss.BossIA.EstadosAtaque.ListoParaAtacar:

                if (ejecutarAtaque)
                {
                    ejecutarAtaque = false;
                    CambiarEstadoAtaqueJugador(EnumBoss.BossIA.EstadosAtaque.Ejecutandoataque);
                }

                break;
            case EnumBoss.BossIA.EstadosAtaque.Ejecutandoataque:

                timerToStartAtk += Time.deltaTime;

                if (timerToStartAtk >= tiempoParaActivarAtaque)
                {
                    timerToStartAtk = 0;
                    ataque(); // void donde se ejecuta el ataque
                    CambiarEstadoAtaqueJugador(EnumBoss.BossIA.EstadosAtaque.PreparandoSiguienteAtaque);
                }

                break;
            case EnumBoss.BossIA.EstadosAtaque.PreparandoSiguienteAtaque:

                timerToAbleToAtk += Time.deltaTime;

                if (timerToAbleToAtk >= tiempoParaPoderVolverAAtacar)
                {
                    timerToAbleToAtk = 0;
                    CambiarEstadoAtaqueJugador(EnumBoss.BossIA.EstadosAtaque.ListoParaAtacar);
                }

                break;
        }
    }


    private void CambiarEstadoAtaqueJugador(EnumBoss.BossIA.EstadosAtaque state)
    {
        estadoenemigoParaAtacar = state;

        switch (state)
        {
            case EnumBoss.BossIA.EstadosAtaque.ListoParaAtacar:

                break;
            case EnumBoss.BossIA.EstadosAtaque.Ejecutandoataque:
                //ObtenerPropiedadesDelAtaque();
                break;
            case EnumBoss.BossIA.EstadosAtaque.PreparandoSiguienteAtaque:

                break;
        }
    }

    private void ataque()
    {
        //GameObject proyectil = Instantiate(prefabAtack, pivoteSpawnAtaque.position, Quaternion.identity);

        //BossAtackProperties caracteristicasProyectil = proyectil.GetComponent<BossAtackProperties>();

        //caracteristicasProyectil.damageProyectil = damageAttack;
        //caracteristicasProyectil.AutoDestruccion(TiempoDestrucciónProyectil);

        //if (padreBoss)
        //{
        //    proyectil.transform.parent = this.transform;
        //}
    }


    private void ObtenerPropiedadesDelAtaque()
    {
        throw new NotImplementedException();
    }
}
