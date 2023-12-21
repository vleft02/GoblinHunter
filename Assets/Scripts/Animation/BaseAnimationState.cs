using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public record AnimationClip(int State, float Duration);

public abstract class BaseAnimationState<EState> where EState : Enum
{
    public EState StateKey { get; set; }
    public int Id { get; set; }
    public float Duration { get; set; }
    public float LockedStateTime { get; set; }

    public AnimationAspectManager AspectManager { get; set; }

    public BaseAnimationState(EState key)
    {
        StateKey = key;
    }

    public void LockState()
    {
        LockedStateTime = Time.time + Duration;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract EState GetNextState();
    
}
