using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopableMovement //base class for movement algorithms that loopable objects adhere to
{
    public abstract IEnumerator ForwardMovement(LoopableObject loopableObject);

    public abstract IEnumerator ReverseMovement(LoopableObject loopableObject);
}
