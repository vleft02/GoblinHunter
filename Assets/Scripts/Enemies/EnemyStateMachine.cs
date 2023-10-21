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

        InitStates();
        CurrentState = States[EnemyState.PATROL];
    }


}
