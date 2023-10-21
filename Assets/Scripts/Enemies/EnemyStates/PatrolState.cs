using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState<EnemyStateMachine.EnemyState>
{
    private int _currentWaypointIndex;
    private EnemyStateMachine _enemy;

    public PatrolState(EnemyStateMachine enemy,
                       EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.PATROL)
            : base(key) 
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {

    }
    
    public override void UpdateState()
    {
        //PatrolCycle();

        //_enemy.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (_enemy.transform.eulerAngles.y < 300 && _enemy.transform.eulerAngles.y > 240)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.LEFT;
        }
        
        if (_enemy.transform.eulerAngles.y > 60 && _enemy.transform.eulerAngles.y < 130)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.RIGHT;
        }

        if (_enemy.transform.eulerAngles.y < 60 || _enemy.transform.eulerAngles.y > 300)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.FRONT;
        }
        
        if (_enemy.transform.eulerAngles.y > 130 && _enemy.transform.eulerAngles.y < 240)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.BACK;
        }
    }

    public override void ExitState()
    {

    }

    private void PatrolCycle()
    {
        if (_enemy.Agent.remainingDistance < 0.2f)
        {
            if (_currentWaypointIndex < _enemy.path.waypoints.Count - 1) _currentWaypointIndex++;
            else _currentWaypointIndex = 0;

            Vector3 nextDestination = _enemy.path.waypoints[_currentWaypointIndex].position;

            if (nextDestination.x < _enemy.transform.position.x)
            {
                AnimationAspectManager._currentAspectKey = Aspects.RIGHT;
            }
            
            if (nextDestination.x > _enemy.transform.position.x)
            {
                AnimationAspectManager._currentAspectKey = Aspects.LEFT;
            }

            _enemy.Agent.SetDestination(nextDestination);
        }
    }


    public override EnemyStateMachine.EnemyState GetNextState()
    {
        // TODO
        return EnemyStateMachine.EnemyState.PATROL;
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




}
