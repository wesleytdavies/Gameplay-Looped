using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : LoopableObject
{
    private BoxCollider2D boxCollider;
    private Animator animator;

    public override void Initialize()
    {
        StartPosition = transform.position;
        EndSize = 80f;
        movementFunction = Stretching;
        DamagePerFrame = 0.25f;

        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator TimeLoop()
    {
        while (true)
        {
            InternalTime = 0f; //reset internal time
            StopCoroutine(reverseCoroutine);
            boxCollider.enabled = false;
            animator.SetInteger("State", 0);
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            //spout for a quarter loop
            InternalTime = 0f; //reset internal time
            boxCollider.enabled = true;
            MoveForward();
            IsReversing = false;
            animator.SetInteger("State", 1);
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            //retract for a quarter loop
            InternalTime = 0f; //reset internal time
            MoveReverse();
            IsReversing = true;
            animator.SetInteger("State", 2);
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
        }
    }

    public override void MoveForward()
    {
        StartCoroutine(forwardCoroutine);
    }
}
