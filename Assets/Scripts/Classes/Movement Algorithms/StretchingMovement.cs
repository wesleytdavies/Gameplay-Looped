using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchingMovement : LoopableMovement //object stretches in one direction (transform.up). used for geysers
{
    private BoxCollider2D collider; //stretching objects must have a box collider
    private Vector2 startSize; //the initial size of the object's collider

    public override void Initialize(LoopableObject loopableObject)
    {
        collider = loopableObject.GetComponent<BoxCollider2D>();
        startSize = collider.size;
    }

    public override IEnumerator ForwardMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            if (loopableObject.InternalTime <= loopableObject.HalfLoopDuration) //this must be in an if statement since the internal time goes one frame past the half loop duration which messes up the easing (object starts to move backwards before it has reversed)
            {
                collider.size = new Vector2(startSize.x, startSize.y * EaseOutQuadraticInterpolate(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration));
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }

    public override IEnumerator ReverseMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            if (loopableObject.InternalTime <= loopableObject.HalfLoopDuration) //this must be in an if statement since the internal time goes one frame past the half loop duration which messes up the easing (object starts to move backwards before it has reversed)
            {
                collider.size = new Vector2(startSize.x, startSize.y / EaseInQuadraticInterpolate(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration));
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }
}
