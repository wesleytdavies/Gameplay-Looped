using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : LoopableMovement //used for objects that move in a straight line at a constant speed
{
    private Vector2 upVector;

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
                Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
                float easingFactor = EaseOutQuadraticFactor(loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                Vector2 forwardVector = currentPosition + upVector * loopableObject.Speed * easingFactor;
                loopableObject.transform.position = forwardVector;
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
                Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
                float easingFactor = EaseInQuadraticFactor(loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                Vector2 reverseVector = currentPosition - upVector * loopableObject.Speed * easingFactor;
                loopableObject.transform.position = reverseVector;
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }
}
