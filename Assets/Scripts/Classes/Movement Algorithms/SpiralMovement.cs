using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMovement : LoopableMovement //creates a circle that expands while the object follows it
{
    private float circleRadius = 1f; //the size of the circle which the object revolves
    private Vector2 upVector;
    private Vector2 circleCenter;
    private float circleExpansionSpeed;

    public override void Initialize(LoopableObject loopableObject)
    {
        upVector = new Vector2(loopableObject.transform.right.x, loopableObject.transform.right.y);
        circleCenter = loopableObject.StartPosition - upVector;
        circleExpansionSpeed = loopableObject.Speed / 10f; //how quickly the circle that the object revolves around expands
    }

    public override IEnumerator ForwardMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            if (loopableObject.InternalTime <= loopableObject.HalfLoopDuration) //this must be in an if statement since the internal time goes one frame past the half loop duration which messes up the easing (object starts to move backwards before it has reversed)
            {
                Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
                //float easingFactor = EaseOutQuadratic(loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                float xDelta = circleCenter.x + Mathf.Cos(loopableObject.InternalTime * loopableObject.Speed * 10f) * circleRadius;
                float yDelta = circleCenter.y - Mathf.Sin(loopableObject.InternalTime * loopableObject.Speed * 10f) * circleRadius;
                loopableObject.transform.position = new Vector3(xDelta, yDelta, 0);
                circleRadius += circleExpansionSpeed;
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
                /*
                Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
                Vector2 upVector = new Vector2(loopableObject.transform.right.x, loopableObject.transform.right.y);
                float easingFactor = EaseInQuadratic(loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                Vector2 reverseVector = currentPosition - upVector * loopableObject.Speed * easingFactor;
                loopableObject.transform.position = reverseVector;
                */
                Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
                //float easingFactor = EaseOutQuadratic(loopableObject.InternalTime / loopableObject.HalfLoopDuration);
                float xDelta = circleCenter.x - Mathf.Cos((loopableObject.HalfLoopDuration - loopableObject.InternalTime) * loopableObject.Speed * 10f) * circleRadius;
                float yDelta = circleCenter.y + Mathf.Sin((loopableObject.HalfLoopDuration - loopableObject.InternalTime) * loopableObject.Speed * 10f) * circleRadius;
                loopableObject.transform.position = new Vector3(xDelta, yDelta, 0);
                circleRadius -= circleExpansionSpeed;
            }
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }
}
