using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackLogicState : BaseState<EnemyStateMachine.EnemyState>
{
    private EnemyStateMachine _enemy;

    public EnemyAttackLogicState(EnemyStateMachine enemy, EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.ATTACK) : base(key)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.StartAttack();
    }

    public override void ExitState()
    {
        _enemy.isAttacking = false;

    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {

        if (!_enemy.PlayerDetected(_enemy.combat_radius) && _enemy.canEndAttack)
        {
            return EnemyStateMachine.EnemyState.CHASE;
        }

        return EnemyStateMachine.EnemyState.ATTACK;
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {

    }


    public override void UpdateState()
    {
    }
}
