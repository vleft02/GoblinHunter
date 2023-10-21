using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Aspects
    {
        FRONT, LEFT, RIGHT, BACK
    }

public class AnimationAspectManager
{
    public static Aspects _currentAspectKey;
    public static bool _changeAspect = false;
}
