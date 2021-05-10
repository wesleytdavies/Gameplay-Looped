using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : LoopableMovement //used for objects that move in a straight line at a constant speed
{
    private Vector2 upVector;
    private Vector2 endPosition;

    public override void Initialize(LoopableObject loopableObject)
    {
        upVector = new Vector2(loopableObject.transform.right.x, loopableObject.transform.right.y);
    }

    public override IEnumerator ForwardMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            if(loopableObject.InternalTime <= loopableObject.HalfLoopDuration) //this must be in an if statement since the internal time goes one frame past the half loop duration which messes up the easing (object starts to move backwards before it has reversed)
            {
                endPosition = loopableObject.StartPosition + upVector * loopableObject.Speed * (loopableObject.HalfLoopDuration / Time.maximumDeltaTime);
                loopableObject.transform.position = EaseOutQuadraticVector2(loopableObject.StartPosition, endPosition, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }

    public override IEnumerator ReverseMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            if(loopableObject.InternalTime <= loopableObject.HalfLoopDuration) //this must be in an if statement since the internal time goes one frame past the half loop duration which messes up the easing (object starts to move backwards before it has reversed)
            {
                endPosition = loopableObject.StartPosition + upVector * loopableObject.Speed * (loopableObject.HalfLoopDuration / Time.maximumDeltaTime);
                loopableObject.transform.position = EaseInQuadraticVector2(endPosition, loopableObject.StartPosition, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }
}
