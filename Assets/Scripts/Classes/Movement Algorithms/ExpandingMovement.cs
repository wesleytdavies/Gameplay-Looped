using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingMovement : LoopableMovement //object expands, useful for explosions
{
    private CircleCollider2D collider; //expanding objects must have a circle collider
    private float startRadius; //the initial radius of the object's collider

    public override void Initialize(LoopableObject loopableObject)
    {
        collider = loopableObject.GetComponent<CircleCollider2D>();
        startRadius = collider.radius;
    }

    public override IEnumerator ForwardMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            if (loopableObject.InternalTime <= loopableObject.HalfLoopDuration) //this must be in an if statement since the internal time goes one frame past the half loop duration which messes up the easing (object starts to move backwards before it has reversed)
            {
                //collider.radius = startRadius * EaseOutQuadraticInterpolate(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                collider.radius = startRadius * Mathf.Lerp(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
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
                //collider.radius = startRadius / EaseInQuadraticInterpolate(1f, loopableObject.EndSize, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                collider.radius = startRadius * Mathf.Lerp(loopableObject.EndSize, 1f, loopableObject.InternalTime / loopableObject.HalfLoopDuration);
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }
}
