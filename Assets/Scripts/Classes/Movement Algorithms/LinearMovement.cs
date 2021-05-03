using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : LoopableMovement //used for objects that move in a straight line at a constant speed
{
    public override IEnumerator ForwardMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
            Vector2 upVector = new Vector2(loopableObject.transform.right.x, loopableObject.transform.right.y);
            Vector2 forwardVector = currentPosition += upVector * loopableObject.Speed;
            loopableObject.transform.position = forwardVector;
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }

    public override IEnumerator ReverseMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
            Vector2 upVector = new Vector2(loopableObject.transform.right.x, loopableObject.transform.right.y);
            Vector2 reverseVector = currentPosition -= upVector * loopableObject.Speed;
            loopableObject.transform.position = reverseVector;
            yield return new WaitForFixedUpdate(); //increment time at a fixed rate to ensure coroutine timings are perfect
        }
    }
}
