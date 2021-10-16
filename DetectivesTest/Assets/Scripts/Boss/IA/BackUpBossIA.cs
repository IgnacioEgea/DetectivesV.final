using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BackUpBossIA : MonoBehaviour
{
    //Referencias Componentes
    NavMeshAgent cmpAgent;
    BossAttackSystem cmpAtkSystem;
    BossManager cmpManager;

    //Referencias others
    GameObject characterReference;

    Vector3 positionBackUp;

    [Header("Referencias")]
    public Transform PosicionInicial;
    public Transform PosicionLight;
    [Header("Parametros")]
    public EnumBoss.BossIA.BossGeneralStates EstadoGeneralActual;
    [Space]
    [Space]
    public EnumBoss.BossIA.BossStates EstadoActual;
    [Space]
    public EnumBoss.BossIA.BossStates EstadoFuturo;
    [Space]
    [Space]
    [Header("Rangos de distancia")]
    public float rangoDeteccionJugador;
    public float rangoAlejamientoPuntoInicial;
    public float rangoAtaqueAlJugador;
    public float rangoPerderVistaAlJugador;

    [Space]
    [Space]

    [Header("Timers")]
    public float TimerParaPasarDeFase; // Timer que se utilizará en los cambios de estado

    [Header("Tiempos")]
    [SerializeField] float TiempoDinamicoParaPasardeFase; //Este sera la vaiable que cambiará dependiedo el estado que se quiera cambiar
    [Space]
    public float timeToReturn;
    public float timeToIdle;
    public float timeToChase;
    public float timeToWeak;
    public float timeToAttack;
    [Tooltip("Tiempo del estado de Weak")]
    public float timeToRepositioning;
    public float timeToSearch;
    public float timeToDeath;
    [Space]
    [Header("Velocidades")]
    public float speedReturn;
    public float speedIdle;
    public float speedChase;
    public float speedWeak;
    public float speedAttack;
    public float speedRepositioning;
    public float speedSearch;
    public float speedDeath;

    [Header("Velocidad Rotación")]
    [Range(0, 8)] public float VelocidadRotacionLookAtObject;
    [Space]
    [Header("Rangos de Alejamiento luz")]
    public float rangoMinAlejamiento;
    public float rangoMaxAlejamiento;
    public Animator bossAnimator;


    public bool Stunned = false;

    private void Awake()
    {
        cmpAgent = GetComponent<NavMeshAgent>();
        cmpAtkSystem = GetComponent<BossAttackSystem>();
        cmpManager = GetComponent<BossManager>();
        bossAnimator = GetComponent<Animator>();
        characterReference = GameObject.FindGameObjectWithTag("Player");

        SetVariablesIniciales();
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
                    break;
                case EnumBoss.BossIA.BossStates.Dialogue:
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
                break;
        }
    }

    private void MovementCombat()
    {
        if (cmpManager.isAlive)
        {
            switch (EstadoActual)
            {
                case EnumBoss.BossIA.BossStates.Return:

                    cmpAgent.SetDestination(PosicionInicial.transform.position);

                    if (DistanceToObject(characterReference) <= rangoDeteccionJugador)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Chase);
                    }

                    if (cmpManager.onLight)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Weak);
                    }

                    if (Vector3.Distance(transform.position, PosicionInicial.transform.position) <= 10)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Idle);
                    }

                    break;
                case EnumBoss.BossIA.BossStates.Idle:

                    if (DistanceToObject(characterReference) <= rangoDeteccionJugador)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Chase);
                    }

                    break;
                case EnumBoss.BossIA.BossStates.Chase:

                    cmpAgent.SetDestination(characterReference.transform.position);

                    if (cmpManager.onLight)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Weak);
                    }
                    else if (DistanceToObject(characterReference) <= rangoAtaqueAlJugador)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Attack);
                    }
                    else if (DistanceToObject(PosicionInicial.gameObject) >= rangoAlejamientoPuntoInicial)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Return);
                    }
                    else if (DistanceToObject(characterReference) >= rangoPerderVistaAlJugador)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Search);
                    }


                    break;
                case EnumBoss.BossIA.BossStates.Weak:

                    TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Repositioning);

                    break;
                case EnumBoss.BossIA.BossStates.Attack:

                    TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Search);

                    break;
                case EnumBoss.BossIA.BossStates.Repositioning:

                    cmpAgent.SetDestination(positionBackUp);

                    if (ExtensionMethods.GetPathRemainingDistance(cmpAgent) < 2)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Search);
                    }
                    break;
                case EnumBoss.BossIA.BossStates.Search:

                    if (DistanceToObject(characterReference) > rangoDeteccionJugador)
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Return);
                    }
                    else
                    {
                        TratarDeCambiarEstado(EnumBoss.BossIA.BossStates.Chase);
                    }

                    break;
                case EnumBoss.BossIA.BossStates.Death:
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
                case EnumBoss.BossIA.BossStates.Return:
                    TiempoDinamicoParaPasardeFase = timeToReturn;
                    break;
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
                case EnumBoss.BossIA.BossStates.Repositioning:
                    TiempoDinamicoParaPasardeFase = timeToRepositioning;
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
            case EnumBoss.BossIA.BossStates.Return:
                SetVariablesReturn();
                bossAnimator.SetBool("isWalking", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isDying", false);

                Stunned = false;
                break;
            case EnumBoss.BossIA.BossStates.Idle:
                SetVariablesIdle();
                bossAnimator.SetBool("isIdle", true);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                break;
            case EnumBoss.BossIA.BossStates.Chase:
                SetVariablesChase();
                bossAnimator.SetBool("isRunning", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                break;
            case EnumBoss.BossIA.BossStates.Weak:
                SetVariablesWeak();
                bossAnimator.SetBool("isStunned", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = true;
                break;
            case EnumBoss.BossIA.BossStates.Attack:
                cmpAtkSystem.ejecutarAtaque = true;
                SetVariablesAttack();
                bossAnimator.SetBool("isAttacking", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isWalking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                break;
            case EnumBoss.BossIA.BossStates.Repositioning:

                positionBackUp =  RandomPointInAnnulus(PosicionLight.transform.position, rangoMinAlejamiento, rangoMaxAlejamiento);
                SetVariablesRepositioning();
                bossAnimator.SetBool("isWalking", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                break;
            case EnumBoss.BossIA.BossStates.Search:
                SetVariablesSearch();
                bossAnimator.SetBool("isWalking", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isDying", false);
                Stunned = false;
                break;
            case EnumBoss.BossIA.BossStates.Death:
                SetVariablesDeath();
                bossAnimator.SetBool("isDying", true);
                bossAnimator.SetBool("isIdle", false);
                bossAnimator.SetBool("isRunning", false);
                bossAnimator.SetBool("isStunned", false);
                bossAnimator.SetBool("isAttacking", false);
                bossAnimator.SetBool("isWalking", false);
                Stunned = false;
                break;
        }
    }

    #region Set variables segun el estado
    private void SetVariablesReturn()
    {
        cmpAgent.speed = speedReturn;
    }
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
    private void SetVariablesRepositioning()
    {
        cmpAgent.speed = speedRepositioning;
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
}
