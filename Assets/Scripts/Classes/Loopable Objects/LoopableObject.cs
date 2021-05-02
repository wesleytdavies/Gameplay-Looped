using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopableObject : MonoBehaviour //base class for all objects that are affected by the time loop
{
    public Vector2 StartPosition //the position where the object begins its loop
    {
        get
        {
            return _startPosition;
        }
        protected set
        {
            _startPosition = value;
        }
    }
    private Vector2 _startPosition;

    public Vector2 StartDirection //the direction of the object when it begins its loop
    {
        get
        {
            return _startDirection;
        }
        protected set
        {
            _startDirection = value;
        }
    }
    private Vector2 _startDirection;

    public float Speed //the speed at which the object travels
    {
        get
        {
            return _speed;
        }
        protected set
        {
            _speed = value;
        }
    }
    private float _speed;

    public LoopableMovement MovementFunction //the movement algorithm the object adheres to
    {
        get
        {
            return _movementFunction;
        }
        protected set
        {
            _movementFunction = value;
        }
    }
    private LoopableMovement _movementFunction;

    protected LinearMovement Linear
    {
        get
        {
            return _linear;
        }
        private set
        {
            _linear = value;
        }
    }
    private LinearMovement _linear = new LinearMovement();

    private IEnumerator forwardCoroutine;
    private IEnumerator reverseCoroutine;

    private float internalTime; //the lifespan of the object

    private void Awake()
    {
        Initialize();
        forwardCoroutine = MovementFunction.ForwardMovement(this);
        reverseCoroutine = MovementFunction.ReverseMovement(this);
    }

    private void Start()
    {
        StartCoroutine(TimeLoop());
    }

    private void FixedUpdate()
    {
        internalTime += Time.fixedDeltaTime;
    }

    IEnumerator TimeLoop()
    {
        while (true)
        {
            MoveForward();
            internalTime = 0f; //reset internal time
            yield return new WaitUntil(() => internalTime >= TimeManager.HalfLoopDuration); //wait until half a loop has elapsed
            MoveReverse();
            internalTime = 0f; //reset internal time
            yield return new WaitUntil(() => internalTime >= TimeManager.HalfLoopDuration); //wait until half a loop has elapsed
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
