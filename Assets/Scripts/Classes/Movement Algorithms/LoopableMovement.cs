using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopableMovement //base class for movement algorithms that loopable objects adhere to
{
    public abstract void Initialize(LoopableObject loopableObject);

    public abstract IEnumerator ForwardMovement(LoopableObject loopableObject);

    public abstract IEnumerator ReverseMovement(LoopableObject loopableObject);

    //easing functions provided by: https://gist.github.com/cjddmut/d789b9eb78216998e95c
    public static float EaseInQuadraticInterpolate(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    public static float EaseOutQuadraticInterpolate(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    public static Vector2 EaseInQuadraticVector2(Vector2 start, Vector2 end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    public static Vector2 EaseOutQuadraticVector2(Vector2 start, Vector2 end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }
}
