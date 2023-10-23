
using UnityEngine;

public class EnemyAnimationFSM : AnimationStateManager<EnemyAnimationFSM.EnemyAnimation>
{

    public enum EnemyAnimation
    {
        IDLE, WALK
    }

    void InitStates()
    {
        States.Add(EnemyAnimation.IDLE, new IdleEnemyState(AspectManager));
        States.Add(EnemyAnimation.WALK, new WalkEnemyState(AspectManager));

    }

    private void Awake()
    {
        AspectManager = GetComponent<AnimationAspectManager>();

        InitStates();
        CurrentState = States[EnemyAnimation.WALK];
    }
}
