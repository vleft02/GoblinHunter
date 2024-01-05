using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationFSM : AnimationStateManager<EnemyAnimationFSM.EnemyAnimation>
{
    private EnemyStateMachine stateMachine;

    public enum EnemyAnimation
    {
        IDLE, WALK, HIT, TO_DEATH, ATTACK
    }

    void InitStates()
    {
        States.Add(EnemyAnimation.IDLE, new EnemyIdleState(AspectManager, stateMachine));
        States.Add(EnemyAnimation.WALK, new EnemyWalkState(AspectManager, stateMachine));
        States.Add(EnemyAnimation.HIT, new EnemyHitState(AspectManager, stateMachine));
        States.Add(EnemyAnimation.TO_DEATH, new EnemyToDeathState(AspectManager));
        States.Add(EnemyAnimation.ATTACK, new EnemyAttackState(AspectManager, stateMachine));

    }

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        AspectManager = GetComponent<AnimationAspectManager>();
        EventManager.EnemyHitEvent += GetHit;
        EventManager.EnemyDeathEvent += ToDeathAnimation;
        InitStates();
        CurrentState = States[EnemyAnimation.IDLE];
    }

    private void OnDisable()
    {
        EventManager.EnemyHitEvent -= GetHit;
        EventManager.EnemyDeathEvent -= ToDeathAnimation;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    
    public void GetHit(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy && CurrentState.StateKey != EnemyAnimation.TO_DEATH)
        {
            TransitionToState(EnemyAnimation.HIT);
        }
    }
    
    
    public void ToDeathAnimation(GameObject enemy)
    {
        if (gameObject == enemy && !TerminateFSM)
        {
            TransitionToState(EnemyAnimation.TO_DEATH);
            TerminateFSM = true;
        }
    }

    public void EndAttackAnimation()
    {
        stateMachine.isAttacking = false;


    }

    private void LateUpdate()
    {
        //if (CurrentState.StateKey == GoblinAnimation.WALK)
        //{
        //    _animator.speed = stateMachine.Agent.desiredVelocity.magnitude * 0.5f;

        //}
        //else
        //{
        //    _animator.speed = 1f;
        //}

    }

}