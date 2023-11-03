using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public enum EnemyState
    {
        ATTACK, PATROL, CHASE
    }

    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player;
    public Path path;

    void InitStates()
    {
        States.Add(EnemyState.ATTACK, new AttackEnemyState());
        States.Add(EnemyState.PATROL, new PatrolEnemyState(this));
        States.Add(EnemyState.CHASE, new ChaseEnemyState(this));
    }

    private void Awake()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        InitStates();
        CurrentState = States[EnemyState.PATROL];
    }

    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
        //transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    private void OnDrawGizmos()
    {
        // Draw a wireframe sphere to represent the detection radius in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PatrolEnemyState.nextWaypoint, 1f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 7f);
    }

    public bool PlayerDetected(float detection_radius)
    {
        // Perform a sphere cast to check for objects within the detection radius
        Collider[] hits = Physics.OverlapSphere(transform.position, detection_radius);

        foreach (Collider hit in hits)
        {
            Vector3 playerPosition = hit.transform.position;

            // Check if the player's position is on the NavMesh
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(playerPosition, out navHit, 1.0f, NavMesh.AllAreas) && hit.CompareTag("Player"))
            {
                // The player is on the NavMesh, you can proceed with your logic here
                // For example, trigger an event, follow the player, etc.
                return true;
            }
        }
        return false;
    }
}
