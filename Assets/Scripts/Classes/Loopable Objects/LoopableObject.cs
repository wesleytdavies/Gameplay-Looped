using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopableObject : MonoBehaviour //base class for all objects that are affected by the time loop
{
    public Vector2 StartPosition //the position where the object begins its loop
    {
        get => _startPosition;
        private set => _startPosition = value;
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

    protected IEnumerator forwardCoroutine;
    protected IEnumerator reverseCoroutine;

    public float InternalTime //the lifespan of the object
    {
        get => _internalTime;
        protected set => _internalTime = value;
    }
    private float _internalTime;

    public bool IsReversing //whether the object is reversing on its loop
    {
        get => _isReversing;
        protected set => _isReversing = value;
    }
    private bool _isReversing;

    public float DamagePerFrame //how much damage this object inflicts per frame of overlap
    {
        get => _damagePerFrame;
        protected set => _damagePerFrame = value;
    }
    private float _damagePerFrame;

    public float EndSize //if changing sizes, how many times bigger/smaller this object is at the end of its loop
    {
        get => _endSize;
        protected set => _endSize = value;
    }
    private float _endSize;

    public bool isPowered; //if yes, the speed of this loopable object is increased by a factor of powerShotFactor
    protected static readonly float powerShotFactor = 1.5f; //powered shots are 1.5 times faster than normal shots

    protected LoopableMovement movementFunction; //the movement algorithm the object adheres to

    #region movement functions
    protected LinearMovement Linear
    {
        get => _linear;
        private set => _linear = value;
    }
    private LinearMovement _linear = new LinearMovement();
    protected SpiralMovement Spiral
    {
        get => _spiral;
        private set => _spiral = value;
    }
    private SpiralMovement _spiral = new SpiralMovement();
    protected ExpandingMovement Expanding
    {
        get => _expanding;
        private set => _expanding = value;
    }
    private ExpandingMovement _expanding = new ExpandingMovement();
    protected StretchingMovement Stretching
    {
        get => _stretching;
        private set => _stretching = value;
    }
    private StretchingMovement _stretching = new StretchingMovement();
    #endregion

    public GameObject originator; //the gameobject that brought this loopable object into existence (i.e. the gun that fired this bullet)
    private Animator animator;

    private void Awake()
    {
        Initialize();
        StartPosition = transform.position;
        movementFunction.Initialize(this);
        forwardCoroutine = movementFunction.ForwardMovement(this);
        reverseCoroutine = movementFunction.ReverseMovement(this);
    }

    private void Start()
    {
        if (isPowered)
        {
            Speed *= powerShotFactor;
            DamagePerFrame *= powerShotFactor;
        }
        StartCoroutine(TimeLoop());
    }

    private void FixedUpdate()
    {
        InternalTime += Time.fixedDeltaTime;
    }

    protected virtual IEnumerator TimeLoop()
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
