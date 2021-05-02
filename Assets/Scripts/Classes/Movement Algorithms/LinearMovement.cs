using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : LoopableMovement //used for objects that move in a straight line
{
    public override IEnumerator ForwardMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
            Vector2 forwardVector = currentPosition += loopableObject.StartDirection * loopableObject.Speed;
            loopableObject.transform.position = forwardVector;
            yield return new WaitForFixedUpdate();
        }
    }
    public override IEnumerator ReverseMovement(LoopableObject loopableObject)
    {
        while (true)
        {
            Vector2 currentPosition = new Vector2(loopableObject.transform.position.x, loopableObject.transform.position.y);
            Vector2 reverseVector = currentPosition -= loopableObject.StartDirection * loopableObject.Speed;
            loopableObject.transform.position = reverseVector;
            yield return new WaitForFixedUpdate();
        }
    }
}
