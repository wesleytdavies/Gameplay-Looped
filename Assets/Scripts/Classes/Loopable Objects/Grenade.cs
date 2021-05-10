using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : LoopableObject
{
    public static readonly string prefabName = "Grenade"; //name of prefab in the Resources folder

    public override void Initialize()
    {
        StartPosition = transform.position;
        Speed = .25f;
        EndSize = 10f;
        movementFunction = Linear;
        DamagePerSecond = 1f;

        HalfLoopDuration = 0.5f;
    }

    protected override IEnumerator TimeLoop()
    {
        while (true)
        {
            InternalTime = 0f; //reset internal time
            MoveForward();
            IsReversing = false;
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            movementFunction = Expanding;
            forwardCoroutine = movementFunction.ForwardMovement(this);
            reverseCoroutine = movementFunction.ReverseMovement(this);
            MoveForward();
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            MoveReverse();
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            movementFunction = Linear;
            forwardCoroutine = movementFunction.ForwardMovement(this);
            reverseCoroutine = movementFunction.ReverseMovement(this);
            MoveReverse();
            IsReversing = true;
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
        }
    }
}
