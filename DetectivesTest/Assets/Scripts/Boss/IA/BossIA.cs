using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class BossIA : MonoBehaviour
{
    //Referencias Componentes
    NavMeshAgent cmpAgent;
    BossAttackSystem cmpAtkSystem;
    BossManager cmpManager;
    Animator bossAnimator;
    //Referencias others
    GameObject characterReference;
    public GameObject Shield;
    Vector3 positionBackUp;

    [Header("Referencias")]
    public Transform PosicionInicial;
    public Transform PosicionLight;
    [Header("Parametros")]
    public EnumBoss.BossIA.BossGeneralStates EstadoGeneralActual;
    [Space]
    [Space]
    public EnumBoss.BossIA.BossStages FaseBossActual;
    [Space]
    [Space]
    [Space]
    public EnumBoss.BossIA.BossStates EstadoActual;
    [Space]
    public EnumBoss.BossIA.BossStates EstadoFuturo;
    [Space]
    [Space]
    [Header("Rangos de distancia")]
    public float rangoDeteccionJugador;
    //public float rangoAlejamientoPuntoInicial;
    public float rangoAtaqueAlJugador;
    //public float rangoPerderVistaAlJugador;

    [Space]
    [Space]

    [Header("Timers")]
    public float TimerParaPasarDeFase; // Timer que se utilizará en los cambios de estado

    [Header("Tiempos")]
    [SerializeField] float TiempoDinamicoParaPasardeFase; //Este sera la vaiable que cambiará dependiedo el estado que se quiera cambiar
    [Space]
    public float timeToIdle;
    public float timeToChase;
    public float timeToWeak;
    public float timeToAttack;
    [Tooltip("Tiempo del estado de Weak")]
    public float timeToSearch;
    public float timeToDeath;
    [Space]
    [Header("Velocidades")]
    public float speedIdle;
    public float speedWeak;
    public float speedAttack;
    public float speedSearch;
    public float speedDeath;
    [Space]
    public float speedChase;
    [Space]
    public float speedChase1;
    public float speedChase2;
    public float speedChase3;

    [Header("Velocidad Anim Ataque")]
    public float AnimAttackspeed;
    [Space]
    public float AnimAttackspeed1;
    public float AnimAttackspeed2;
    public float AnimAttackspeed3;
    [Space]
    [Space]
    [Header("tiempo Stum")]
    public float TiempoStumActual;
    [Space]
    public float tiempoStum1;
    public float tiempoStum2;
    public float tiempoStum3;
    float timerStun;
    public bool Stunned = false;

    private void Awake()
    {
        cmpAgent = GetComponent<NavMeshAgent>();
        cmpAtkSystem = GetComponent<BossAttackSystem>();
        cmpManager = GetComponent<BossManager>();
        bossAnimator = GetComponent<Animator>();
        characterReference = GameObject.FindGameObjectWithTag("Player");

        SetVariablesIniciales();
        CambiarGeneralEstado(EnumBoss.BossIA.BossGeneralStates.Pre);

    }



    private void Update()
    {
       

        switch (EstadoGeneralActual)
        {
            case EnumBoss.BossIA.BossGeneralStates.Pre:
                MovementPreCombat();
                break;
            case EnumBoss.BossIA.BossGeneralStates.Battle:
                MovementCombat();
                break;
            case EnumBoss.BossIA.BossGeneralStates.Post:
                MovementPostCombat();
                break;
        }
    }

    private void MovementPreCombat()
    {
        if (cmpManager.isAlive)
        {
            switch (EstadoActual)
            {
                case EnumBoss.BossIA.BossStates.Idle:
                    //Puede hablar
                    break;
                case EnumBoss.BossIA.BossStates.Dialogue:
                    //Esta hablando
                    break;
                case EnumBoss.BossIA.BossStates.Default:
                    break;
            }
        }
    }

    private void MovementPostCombat()
    {
        switch (EstadoActual)
        {
            case EnumBoss.BossIA.BossStates.Death:

                //Esta muerto

                break;
        }
    }

    private void MovementCombat()
    {
        if (cmpManager.isAlive)
        {
            switch (EstadoActual)
            {
                case EnumBoss.BossIA.BossStates.Chase:

                    cmpAgent.SetDestination(characterReference.transform.position);

                    if (cmpManager.onLight /*&& cmpManager.vulnerableLuz*/)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Weak);
                    }
                    else if (DistanceToObject(characterReference) <= rangoAtaqueAlJugador)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Attack);
                    }
                    break;
                case EnumBoss.BossIA.BossStates.Weak:

                    timerStun += Time.deltaTime;

                    if (timerStun >= TiempoStumActual)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Chase);
                    }

                    break;
                case EnumBoss.BossIA.BossStates.Attack:

                    TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Search);

                    break;
                case EnumBoss.BossIA.BossStates.Search:

                    if (DistanceToObject(characterReference) <= rangoAtaqueAlJugador)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Attack);
                    }
                    else
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Chase);
                    }


                    break;
            }
        }
    }



    void TratarDeCambiarEstado(EnumBoss.BossIA.BossStates EstadoACambiar)
    {
        if (EstadoACambiar != EstadoFuturo) //Si se quiere cambiar de estado
        {
            TimerParaPasarDeFase = 0; // se resetea el contador ya que se quiere cambiar a otro estado diferente

            switch (EstadoACambiar)
            {
                case EnumBoss.BossIA.BossStates.Idle:
                    TiempoDinamicoParaPasardeFase = timeToIdle;
                    break;
                case EnumBoss.BossIA.BossStates.Chase:
                    TiempoDinamicoParaPasardeFase = timeToChase;
                    break;
                case EnumBoss.BossIA.BossStates.Weak:
                    TiempoDinamicoParaPasardeFase = timeToWeak;
                    break;
                case EnumBoss.BossIA.BossStates.Attack:
                    TiempoDinamicoParaPasardeFase = timeToAttack;
                    break;
                case EnumBoss.BossIA.BossStates.Search:
                    TiempoDinamicoParaPasardeFase = timeToSearch;
                    break;
                case EnumBoss.BossIA.BossStates.Death:
                    TiempoDinamicoParaPasardeFase = timeToDeath;
                    break;
            }

            EstadoFuturo = EstadoACambiar;

        }

        TimerParaPasarDeFase += Time.deltaTime;

        if (TimerParaPasarDeFase >= TiempoDinamicoParaPasardeFase)
        {
            cambiarEstado(EstadoFuturo);
        }
    }

    void cambiarEstado(EnumBoss.BossIA.BossStates EstadoACambiar)
    {
        TimerParaPasarDeFase = 0;

        EstadoActual = EstadoACambiar;

        switch (EstadoACambiar)
        {
            case EnumBoss.BossIA.BossStates.Idle:
                SetVariablesIdle();
                Shield.SetActive(true);
                bossAnimator.SetBool("isIdle", true);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                bossAnimator.speed = 1;
                break;
            case EnumBoss.BossIA.BossStates.Chase:
                SetVariablesChase();
                Shield.SetActive(true);
                bossAnimator.SetBool("isRunning", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                bossAnimator.speed = 1;

                timerStun = 0; // resetTimer
                break;
            case EnumBoss.BossIA.BossStates.Weak:
                SetVariablesWeak();
                Shield.SetActive(false);
                bossAnimator.SetBool("isStunned", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = true;
                bossAnimator.speed = 1;
                cmpManager.vulnerableLuz = false;
                break;
            case EnumBoss.BossIA.BossStates.Attack:
                cmpAtkSystem.ejecutarAtaque = true;
                SetVariablesAttack();
                Shield.SetActive(true);
                bossAnimator.SetBool("isAttacking", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                bossAnimator.speed = AnimAttackspeed;
                break;
            case EnumBoss.BossIA.BossStates.Search:
                SetVariablesSearch();
                Shield.SetActive(true);
                bossAnimator.SetBool("isWalking", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                bossAnimator.speed = 1;
                break;
            case EnumBoss.BossIA.BossStates.Death:
                SetVariablesDeath();
                Shield.SetActive(true);
                bossAnimator.SetBool("isDying", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                Stunned = false;
                bossAnimator.speed = 1;
                break;
        }
    }


    public void CambiarGeneralEstado(EnumBoss.BossIA.BossGeneralStates state)
    {
        EstadoGeneralActual = state;
        Debug.Log(state);

        switch (state)
        {
            case EnumBoss.BossIA.BossGeneralStates.Pre:

                cambiarEstado(EnumBoss.BossIA.BossStates.Idle);
                EstadoFuturo = EnumBoss.BossIA.BossStates.Default;

                break;
            case EnumBoss.BossIA.BossGeneralStates.Battle:

                CambiarFaseBoss(EnumBoss.BossIA.BossStages.Normal);
                cambiarEstado(EnumBoss.BossIA.BossStates.Chase);
                EstadoFuturo = EnumBoss.BossIA.BossStates.Default;

                break;
            case EnumBoss.BossIA.BossGeneralStates.Post:

                cambiarEstado(EnumBoss.BossIA.BossStates.Death);
                EstadoFuturo = EnumBoss.BossIA.BossStates.Default;

                break;
        }
    }

    public void CambiarFaseBoss(EnumBoss.BossIA.BossStages stage)
    {
        FaseBossActual = stage;

        switch (stage)
        {
            case EnumBoss.BossIA.BossStages.Normal:

                speedChase = speedChase1;
                AnimAttackspeed = AnimAttackspeed1;
                TiempoStumActual = tiempoStum1;

                SetVariablesChase();

                break;
            case EnumBoss.BossIA.BossStages.HalfLife:

                speedChase = speedChase2;
                AnimAttackspeed = AnimAttackspeed2;
                TiempoStumActual = tiempoStum2;

                SetVariablesChase();

                break;
            case EnumBoss.BossIA.BossStages.LowLife:

                speedChase = speedChase3;
                AnimAttackspeed = AnimAttackspeed3;
                TiempoStumActual = tiempoStum3;

                SetVariablesChase();

                break;
        }

    }



    #region Set variables segun el estado

    private void SetVariablesIdle()
    {
        cmpAgent.speed = speedIdle;
    }
    private void SetVariablesChase()
    {
        cmpAgent.speed = speedChase;
    }
    private void SetVariablesWeak()
    {
        cmpAgent.speed = speedWeak;
    }
    private void SetVariablesAttack()
    {
        cmpAgent.speed = speedAttack;
    }
    private void SetVariablesSearch()
    {
        cmpAgent.speed = speedSearch;
    }
    private void SetVariablesDeath()
    {
        cmpAgent.speed = speedDeath;
    }
    #endregion segun estado

    private void SetVariablesIniciales()
    {
        cambiarEstado(EnumBoss.BossIA.BossStates.Return);

        if (PosicionInicial == null)
        {
            PosicionInicial = this.transform;
        }
    }

    #region Herramientas Extra
    private float DistanceToObject(GameObject objetivo)
    {
        return Vector3.Distance(this.transform.position, objetivo.transform.position);
    }


    public Vector3 RandomPointInAnnulus(Vector3 origin, float minRadius, float maxRadius)
    {

        Vector3 randomDirection = (UnityEngine.Random.insideUnitCircle * origin).normalized;

        var randomDistance = UnityEngine.Random.Range(minRadius, maxRadius);

        Vector3 point = origin + randomDirection * randomDistance;

        return point;
    }
    #endregion
}

public static class ExtensionMethods
{
    public static float GetPathRemainingDistance(this NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }
}
