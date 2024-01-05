using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : BaseState<EnemyStateMachine.EnemyState>
{
    private float speed = 2f;
    private EnemyStateMachine _enemy;
    public EnemyChaseState(EnemyStateMachine enemy, EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.CHASE) : base(key)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.Agent.isStopped = false;
        _enemy.Agent.speed = speed;
    }

    public override void ExitState()
    {

    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {
        if (_enemy.PlayerDetected(_enemy.combat_radius))
        {
            return EnemyStateMachine.EnemyState.ATTACK;
        }
        if (!_enemy.PlayerDetected(_enemy.chase_radius))
        {
            return EnemyStateMachine.EnemyState.PATROL;
        }

        return EnemyStateMachine.EnemyState.CHASE;
    }

    public override void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        // Don't just chase, find the best way
        _enemy.Agent.SetDestination(_enemy.Player.transform.position);
    }
}
