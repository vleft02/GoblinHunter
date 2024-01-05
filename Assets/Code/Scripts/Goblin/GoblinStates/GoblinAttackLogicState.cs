using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackLogicState : BaseState<GoblinStateMachine.GoblinState>
{
    private GoblinStateMachine _enemy;

    public GoblinAttackLogicState(GoblinStateMachine enemy, GoblinStateMachine.GoblinState key = GoblinStateMachine.GoblinState.ATTACK) : base(key)
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

    public override GoblinStateMachine.GoblinState GetNextState()
    {

        if (!_enemy.PlayerDetected(_enemy.combat_radius) && _enemy.canEndAttack)
        {
            return GoblinStateMachine.GoblinState.CHASE;
        }

        return GoblinStateMachine.GoblinState.ATTACK;
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
