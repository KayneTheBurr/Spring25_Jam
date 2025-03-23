using System.Collections;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public enum EnemyStates
    {
        IDLE,
        ALERTED,
        PATROL,
        RUNAWAY,
        FOLLOW,
        CHARGEUP,
        ATTACK,
        DEAD
    }
    public EnemyStates enemyState;
    private NavMeshAgent agent;

    //Variables Here
    public float speed;
    public float damage;
    private Transform playerTransform;
    public float dmgFromHitbox = 5f;
    private Transform spriteObj;
    private Animator spriteAnim;

    [Header("Idle State")]
    public float maxIdleTime;
    public float minIdleTime;
    public float idleTime;

    [Header("Alerted State")]
    public float timeSpentAlert;
    public float maxSightRange;
    private Coroutine alertedRoutine;

    [Header("Patrol State")]
    public float minPatrolDistance;
    public float maxPatrolDistance;
    private Vector3 patrolTarget;
    public float minDistanceToTarget = 0.1f;

    [Header("Runaway State")]
    

    [Header("Follow State")]
    

    [Header("Attack State")]
    public float minAttackDistance;

    private void Awake()
    {
        spriteObj = transform.GetComponentInChildren<SpriteRenderer>().transform;
        spriteAnim = transform.GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        OnEnable();
    }

    public virtual void Start()
    {
        agent.speed = speed;

        playerTransform = FindFirstObjectByType<PlayerMovementManager>().gameObject.transform;

        SetState(EnemyStates.IDLE);

        OnGameStateChanged();
    }

    public virtual void Update()
    {
        switch (enemyState)
        {
            case EnemyStates.IDLE:
                IdleUpdate();
                if (GetDistanceFromPlayer() < maxSightRange)
                {
                    SetState(EnemyStates.ALERTED);
                }
                break;
            case EnemyStates.ALERTED:
                break;
            case EnemyStates.PATROL:
                PatrolUpdate();
                if (GetDistanceFromPlayer() < maxSightRange)
                {
                    SetState(EnemyStates.ALERTED);
                }
                break;
            case EnemyStates.RUNAWAY:
                RunawayUpdate();
                break;
            case EnemyStates.FOLLOW:
                FollowUpdate();
                break;
            case EnemyStates.CHARGEUP:
                ChargeUpUpdate();
                break;
            case EnemyStates.ATTACK:
                AttackUpdate();
                break;
            case EnemyStates.DEAD:
                break;
            default:
                Debug.Log("State doesn't exist in enemy, add it to the enum.");
                break;
        }

        SetVisuals();
    }

    #region Listeners

    public void OnEnable()
    {
        WorldGameState.worldStateChanged += OnGameStateChanged;
    }

    public void OnDisable()
    {
        WorldGameState.worldStateChanged -= OnGameStateChanged;
    }

    public void OnGameStateChanged()
    {
        DrugState gameState = WorldGameState.GetWorldState();

        switch (gameState)
        {
            case DrugState.Kikki:
                spriteAnim.SetFloat("worldState", 0);
                if(enemyState == EnemyStates.RUNAWAY)
                {
                    SetState(EnemyStates.ALERTED);
                }
                break;
            case DrugState.Bouba:
                spriteAnim.SetFloat("worldState", 1);
                if (enemyState == EnemyStates.FOLLOW)
                {
                    SetState(EnemyStates.ALERTED);
                }
                break;
            default:
                Debug.Log("No world game state, cannot fact check enemy.");
                break;
        }
    }

    #endregion

    //called in update
    private void SetVisuals()
    {
        // going left
        if(transform.forward.x < 0)
        {
            spriteObj.localScale = new Vector3(-1, 1, 1);
        }
        else if(transform.forward.x > 0) //going right
        {
            spriteObj.localScale = new Vector3(1, 1, 1);
        }

        spriteAnim.SetBool("isMoving", agent.velocity != Vector3.zero);
    }

    public void SetState(EnemyStates newEnemyState)
    {
        enemyState = newEnemyState;

        switch (enemyState)
        {
            case EnemyStates.IDLE:
                idleTime = Random.Range(minIdleTime, maxIdleTime);
                break;
            case EnemyStates.ALERTED:
                if(alertedRoutine == null)
                {
                    alertedRoutine = StartCoroutine(AlertEnemy());
                }
                break;
            case EnemyStates.PATROL:
                if (!FindRandomPatrolPointOnNavMesh()) { SetState(EnemyStates.IDLE); }
                break;
            case EnemyStates.RUNAWAY:
                break;
            case EnemyStates.FOLLOW:
                break;
            case EnemyStates.CHARGEUP:
                break;
            case EnemyStates.ATTACK:
                SetAttackState();
                break;
            case EnemyStates.DEAD:
                Die();
                break;
            default:
                Debug.Log("State doesn't exist in enemy, add it to the enum.");
                break;
        }
    }

    IEnumerator AlertEnemy()
    {
        // play alert animation
        agent.SetDestination(transform.position);
        spriteAnim.SetTrigger("alerted");

        yield return new WaitForSeconds(timeSpentAlert);

        // check if in bouba or kiki
        // set to either follow or runaway

        DrugState worldState = WorldGameState.GetWorldState();

        switch (worldState)
        {
            case DrugState.Bouba:
                SetState(EnemyStates.RUNAWAY);
                break;
            case DrugState.Kikki:
                SetState(EnemyStates.FOLLOW);
                break;
            default:
                Debug.Log("No world game state.");
                SetState(EnemyStates.IDLE);
                break;
        }

        alertedRoutine = null;
    }

    private bool FindRandomPatrolPointOnNavMesh()
    {
        for (int i = 0; i < 30; i++)
        {
            float patrolDistance = Random.Range(minPatrolDistance, maxPatrolDistance);
            Vector3 patrolDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            patrolDir.Normalize();
            Vector3 patrolPoint = transform.position + (patrolDir * patrolDistance);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(patrolPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                patrolTarget = hit.position;
                return true;
            }
        }

        return false;
    }

    private void Die()
    {
        GetComponent<Collider>().enabled = false;

        // death animation
    }

    #region State Updates

    public void IdleUpdate()
    {
        if (idleTime > 0f)
        {
            idleTime -= Time.deltaTime;

            if (idleTime <= 0f)
            {
                SetState(EnemyStates.PATROL);
            }
        }
    }

    public void PatrolUpdate()
    {
        agent.SetDestination(patrolTarget);

        float distToPatrolTarget = Vector3.Distance(patrolTarget, transform.position);
        if(distToPatrolTarget <= minDistanceToTarget)
        {
            SetState(EnemyStates.IDLE);
        }
    }

    public void RunawayUpdate()
    {
        // move away from player
        Vector3 playerPos = playerTransform.position;
        Vector3 ourPos = transform.position;
        Vector3 targetDir = playerPos - ourPos;
        targetDir.x = -targetDir.x;
        targetDir.z = -targetDir.z;
        targetDir.Normalize();

        float targetDist = GetDistanceFromPlayer();

        Vector3 targetPos = ourPos + (targetDir * targetDist);

        agent.SetDestination(targetPos);

        // change state based on player distance
        float distFromPlayer = GetDistanceFromPlayer();

        if (distFromPlayer <= minAttackDistance)
        {
            SetState(EnemyStates.CHARGEUP);
            return;
        }

        if (distFromPlayer > maxSightRange)
        {
            SetState(EnemyStates.IDLE);
            return;
        }
    }

    public void FollowUpdate()
    {
        // chase after player
        Vector3 playerPos = playerTransform.position;

        agent.SetDestination(playerPos);

        // change state based on player distance
        float distFromPlayer = GetDistanceFromPlayer();

        if(distFromPlayer <= minAttackDistance)
        {
            SetState(EnemyStates.CHARGEUP);
            return;
        }

        if(distFromPlayer > maxSightRange)
        {
            SetState(EnemyStates.IDLE);
            return;
        }
    }

    public void ChargeUpUpdate()
    {
        agent.SetDestination(transform.position);
        // charge up animation

        //temp
        EndChargeUpState();
    }

    // called from charge up animation
    public void EndChargeUpState()
    {
        SetState(EnemyStates.ATTACK);
    }

    public virtual void AttackUpdate()
    {

    }

    public virtual void SetAttackState()
    {
        // attack animation

        //temp
        EndAttackState();
    }

    // called from attack animation
    public void EndAttackState()
    {
        SetState(EnemyStates.IDLE);
    }

    #endregion

    #region Utility

    private float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(playerTransform.position, this.transform.position);
        return distance;
    }

    #endregion
}
