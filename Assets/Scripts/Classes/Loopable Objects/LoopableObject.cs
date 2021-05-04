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
    private float _halfLoopDuration = 1f; //the default one way time is 1 seconds

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

    public float InternalTime //the lifespan of the object
    {
        get => _internalTime;
        private set => _internalTime = value;
    }
    private float _internalTime;

    public bool IsReversing //whether the object is reversing on its loop
    {
        get => _isReversing;
        private set => _isReversing = value;
    }
    private bool _isReversing;

    public GameObject originator; //the gameobject that brought this loopable object into existence (i.e. the gun that fired this bullet)

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
        InternalTime += Time.fixedDeltaTime;
        //InternalTime += Mathf.Clamp(Time.fixedDeltaTime, 0f, HalfLoopDuration); //increment time at a fixed rate to ensure coroutine timings are perfect. Clamp to prevent internal time from ever surpassing half loop duration, which would mess up the easing functions
    }

    IEnumerator TimeLoop()
    {
        while (true)
        {
            InternalTime = 0f; //reset internal time
            MoveForward();
            IsReversing = false;
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
            InternalTime = 0f; //reset internal time
            MoveReverse();
            IsReversing = true;
            yield return new WaitUntil(() => InternalTime >= HalfLoopDuration); //wait until half a loop has elapsed
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
