using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenade : LoopableObject
{
    public static readonly string prefabName = "Enemy Grenade"; //name of prefab in the Resources folder

    private Animator animator;

    public override void Initialize()
    {
        StartPosition = transform.position;
        Speed = .25f;
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
            MoveForward();
            IsReversing = false;
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            StopCoroutine(forwardCoroutine);
            movementFunction = Expanding;
            DamagePerFrame = 3f;
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
            DamagePerFrame = 1f;
            forwardCoroutine = movementFunction.ForwardMovement(this);
            reverseCoroutine = movementFunction.ReverseMovement(this);
            MoveReverse();
            IsReversing = true;
            animator.SetInteger("State", 0);
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
        }
    }
}
