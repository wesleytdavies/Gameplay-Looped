using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopableObject : MonoBehaviour //base class for all objects that are affected by the time loop
{
    public Vector2 StartPosition //the position where the object begins its loop
    {
        get => _startPosition;
        protected set => _startPosition = value;
    }
    private Vector2 _startPosition;

    public float Speed //the speed at which the object travels
    {
        get => _speed;
        protected set => _speed = value;
    }
    private float _speed;

    public float HalfLoopDuration //the duration of one way in the time loop in seconds
    {
        get =>_halfLoopDuration;
        protected set => _halfLoopDuration = value;
    }
    private float _halfLoopDuration = 3f; //the default one way time is 3 seconds

    protected LoopableMovement movementFunction; //the movement algorithm the object adheres to

    //references to all movement functions:
    protected LinearMovement Linear
    {
        get => _linear;
        private set => _linear = value;
    }
    private LinearMovement _linear = new LinearMovement();

    private IEnumerator forwardCoroutine;
    private IEnumerator reverseCoroutine;

    private float internalTime; //the lifespan of the object

    private void Awake()
    {
        Initialize();
        forwardCoroutine = movementFunction.ForwardMovement(this);
        reverseCoroutine = movementFunction.ReverseMovement(this);
    }

    private void Start()
    {
        StartCoroutine(TimeLoop());
    }

    private void FixedUpdate()
    {
        internalTime += Time.fixedDeltaTime; //increment time at a fixed rate to ensure coroutine timings are perfect
    }

    IEnumerator TimeLoop()
    {
        while (true)
        {
            MoveForward();
            internalTime = 0f; //reset internal time
            yield return new WaitUntil(() => internalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            MoveReverse();
            internalTime = 0f; //reset internal time
            yield return new WaitUntil(() => internalTime >= HalfLoopDuration); //wait until half a loop has elapsed
        }
    }

    public abstract void Initialize();

    public virtual void MoveForward()
    {
        StopCoroutine(reverseCoroutine);
        StartCoroutine(forwardCoroutine);
    }

    public virtual void MoveReverse()
    {
        StopCoroutine(forwardCoroutine);
        StartCoroutine(reverseCoroutine);
    }
}
