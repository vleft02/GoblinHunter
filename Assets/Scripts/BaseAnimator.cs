using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimator : MonoBehaviour
{
    protected abstract int LockState(int state, float lt);

    protected abstract int GetState();

    protected abstract void ChangeAnimationState(int state);
}
