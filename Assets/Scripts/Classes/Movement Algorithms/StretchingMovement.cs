using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchingMovement : LoopableMovement //object stretches in one direction (transform.up). used for geysers
{
    private BoxCollider2D collider; //stretching objects must have a box collider
    private Vector2 startSize; //the initial size of the object's collider
    private float endHeight; //the final y-value of the object's collider. different from loopableObject.EndSize since EndSize is a scalar, not a value
    private float offsetY; //the y-offset of the box collider

    public override void Initialize(LoopableObject loopableObject)
    {
        collider = loopableObject.GetComponent<BoxCollider2D>();
        startSize = collider.size;
        endHeight = collider.size.y * loopableObject.EndSize;
    }

    public override IEnumerator ForwardMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            if (loopableObject.InternalTime <= loopableObject.HalfLoopDuration) //this must be in an if statement since the internal time goes one frame past the half loop duration which messes up the easing (object starts to move backwards before it has reversed)
            {
                //collider.size = new Vector2(startSize.x, startSize.y * EaseOutQuadraticInterpolate(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration));
                collider.size = new Vector2(startSize.x, startSize.y * Mathf.Lerp(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration));
                offsetY = -endHeight / 2 + Mathf.Lerp(startSize.y / 2, endHeight / 2, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                collider.offset = new Vector2(0f, offsetY);
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
                //collider.size = new Vector2(startSize.x, startSize.y / EaseInQuadraticInterpolate(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration));
                collider.size = new Vector2(startSize.x, startSize.y * Mathf.Lerp(loopableObject.EndSize, 1f, loopableObject.InternalTime / loopableObject.HalfLoopDuration));
                offsetY = -endHeight / 2 + Mathf.Lerp(endHeight / 2, startSize.y / 2, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                collider.offset = new Vector2(0f, offsetY);
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }
}
