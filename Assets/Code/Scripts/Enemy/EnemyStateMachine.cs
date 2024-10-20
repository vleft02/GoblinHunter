using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public bool canEndAttack = false;
    public float combat_radius = 2f;
    public float patrol_radius = 18f;
    public float chase_radius = 12f;

    public enum EnemyState
    {
        ATTACK, PATROL, CHASE, DEAD
    }

    private SpriteBillboard billboard;
    private EnemyController controller;
    private bool _isAttacking = false;

    public bool _isIdling = false;
    public bool isAttacking
    {
        get => _isAttacking;
        set => _isAttacking = value;
    }
    private UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent Agent { get => agent; }
    public GameObject Player;

    void InitStates()
    {
        States.Add(EnemyState.ATTACK, new EnemyAttackLogicState(this));
        States.Add(EnemyState.PATROL, new EnemyPatrolState(this));
        States.Add(EnemyState.CHASE, new EnemyChaseState(this));
        States.Add(EnemyState.DEAD, new EnemyDeathState(this));
    }

    private void Awake()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        billboard = GetComponentInChildren<SpriteBillboard>();
        controller = GetComponent<EnemyController>();
        EventManager.EnemyDeathEvent += DropDead;
        Agent.isStopped = true;

        combat_radius = 2f;
        patrol_radius = 18f;
        chase_radius = 12f;

        InitStates();
        CurrentState = States[EnemyState.PATROL];
    }
    private void OnDisable()
    {
        EventManager.EnemyDeathEvent -= DropDead;
    }
    private void LateUpdate()
    {
        if (CurrentState.StateKey != EnemyState.DEAD)
        {

            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            }
        }
    }

    /*
    private void OnDrawGizmos()
    {
        // Draw a wireframe sphere to represent the detection radius in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 7f);
    }
    */

    public bool PlayerDetected(float detection_radius)
    {
        // Perform a sphere cast to check for objects within the detection radius
        Collider[] hits = Physics.OverlapSphere(transform.position, detection_radius);

        foreach (Collider hit in hits)
        {
            Vector3 playerPosition = hit.transform.position;

            // Check if the player's position is on the NavMesh: 2f is enough distance but we have a problem when the player is out of bounds
            UnityEngine.AI.NavMeshHit navHit;
            if (UnityEngine.AI.NavMesh.SamplePosition(playerPosition, out navHit, 2f, UnityEngine.AI.NavMesh.AllAreas) && hit.CompareTag("Player"))
            {
                // The player is on the NavMesh, you can proceed with your logic here
                // For example, trigger an event, follow the player, etc.
                return true;
            }
        }
        return false;
    }

    public void DropDead(GameObject enemy)
    {
        if (gameObject == enemy)
        {
            TransitionToState(EnemyState.DEAD);
            TerminateFSM = true;
            billboard.rotateYAxis = true;
            GetComponent<Collider>().enabled = false;
            // After he dies, just drop
            agent.speed = 0;
        }
    }

    public bool IsInCombatRange()
    {
        return PlayerDetected(combat_radius);
    }

    /// <summary>
    /// Starts Attack function for the enemy from the AttackEnemyState
    /// </summary>
    public void StartAttack()
    {
        //StartCoroutine(AttackAndWait());        
        AttackAndWait();
    }

    /// <summary>
    /// Starts Idle&Wait Coroutine
    /// </summary>
    public void StartIdle()
    {
        StartCoroutine(IdleAndWait());
    }

    public void Attack()
    {
        isAttacking = true;
        agent.isStopped = true;
        agent.speed = 0;
    }

    ///// <summary>
    ///// Enables the attack animation event and attacks the player
    ///// </summary>
    ///// <returns></returns>
    public void AttackAndWait()
    {
        isAttacking = true;
        agent.isStopped = true;
        agent.speed = 0;

    }

    public IEnumerator IdleAndWait()
    {
        Agent.speed = 0;
        _isIdling = true;
        // Wait for the specified idle interval
        yield return new WaitForSeconds(2f);

        Agent.isStopped = true;
    }

    public void CanEndAttack()
    {
        canEndAttack = true;
    }

    public void CannotEndAttack()
    {

        canEndAttack = false;

    }

    // Gets called at the time of impact with player from within the animation (Unity Animation Event System)
    public void DamagePlayer()
    {
        
        if (PlayerDetected(combat_radius))
        {
            EventManager.PlayerTakeHit(controller.damage);

        }

    }
}
