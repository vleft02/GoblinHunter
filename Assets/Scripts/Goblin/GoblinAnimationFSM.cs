using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAnimationFSM;

public class GoblinAnimationFSM : AnimationStateManager<GoblinAnimationFSM.GoblinAnimation>
{
    public enum GoblinAnimation
    {
        IDLE, WALK, HIT, TO_DEATH, ATTACK
    }

    void InitStates()
    {
        States.Add(GoblinAnimation.IDLE, new GoblinIdleState(AspectManager));
        //States.Add(GoblinAnimation.WALK, new WalkEnemyState(AspectManager));
        States.Add(GoblinAnimation.HIT, new GoblinHitState(AspectManager));
        States.Add(GoblinAnimation.TO_DEATH, new GoblinToDeathState(AspectManager));
        States.Add(GoblinAnimation.ATTACK, new GoblinAttackState(AspectManager));

    }

    private void Awake()
    {
        AspectManager = GetComponent<AnimationAspectManager>();
        EventManager.EnemyHitEvent += GetHit;
        EventManager.EnemyDeathEvent += ToDeathAnimation;
        InitStates();
        CurrentState = States[GoblinAnimation.IDLE];
    }

    public Transform GetTransform()
    {
        return transform;
    }

    
    public void GetHit(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy && CurrentState.StateKey != GoblinAnimation.TO_DEATH)
        {
            TransitionToState(GoblinAnimation.HIT);
        }
    }
    
    
    public void ToDeathAnimation(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy && !TerminateFSM)
        {
            TransitionToState(GoblinAnimation.TO_DEATH);
            TerminateFSM = true;
        }
    }


}