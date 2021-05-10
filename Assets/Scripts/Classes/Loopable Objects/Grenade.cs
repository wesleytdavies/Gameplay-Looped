using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : LoopableObject
{
    public static readonly string prefabName = "Grenade"; //name of prefab in the Resources folder

    private Animator animator;

    public override void Initialize()
    {
        StartPosition = transform.position;
        Speed = 1f;
        EndSize = 10f;
        movementFunction = Linear;
        DamagePerFrame = 1f;

        HalfLoopDuration = 0.5f;
        Expanding.Initialize(this);
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator TimeLoop()
    {
        while (true)
        {
            InternalTime = 0f; //reset internal time
            IsReversing = false;
            MoveForward();
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            StopCoroutine(forwardCoroutine);
            movementFunction = Expanding;
            forwardCoroutine = movementFunction.ForwardMovement(this);
            reverseCoroutine = movementFunction.ReverseMovement(this);
            MoveForward();
            animator.SetInteger("State", 1);
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            MoveReverse();
            animator.SetInteger("State", 2);
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            StopCoroutine(reverseCoroutine);
            movementFunction = Linear;
            forwardCoroutine = movementFunction.ForwardMovement(this);
            reverseCoroutine = movementFunction.ReverseMovement(this);
            animator.SetInteger("State", 0);
            IsReversing = true;
            MoveReverse();
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
        }
    }
}
