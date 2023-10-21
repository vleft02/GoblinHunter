
using UnityEngine;

public class EnemyAnimationFSM : AnimationStateManager<EnemyAnimationFSM.EnemyAnimation>
{

    public enum EnemyAnimation
    {
        IDLE, WALK
    }

    void InitStates()
    {
        States.Add(EnemyAnimation.IDLE, new IdleEnemyState());
        States.Add(EnemyAnimation.WALK, new WalkEnemyState());

    }

    private void Awake()
    {

        InitStates();
        CurrentState = States[EnemyAnimation.IDLE];
    }
}
