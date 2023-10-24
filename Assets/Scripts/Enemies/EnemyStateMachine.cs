using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public enum EnemyState
    {
        ATTACK, PATROL
    }

    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }

    [SerializeField]
    public Path path;

    void InitStates()
    {
        States.Add(EnemyState.ATTACK, new AttackEnemyState());
        States.Add(EnemyState.PATROL, new PatrolState(this));
    }

    private void Awake()
    {
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


}
