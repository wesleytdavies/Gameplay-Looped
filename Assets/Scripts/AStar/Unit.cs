using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit to Sebastian Lague on YouTube for pathfinding tutorials
public class Unit : MonoBehaviour
{
    public float recallRadius = 1f;

    public Transform target;
    public GameObject player;
    [SerializeField] private float speed;
    Vector3[] path;
    int targetIndex;
    public Rigidbody2D Rb
    {
        get => _rb;
        private set => _rb = value;
    }
    private Rigidbody2D _rb;

    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        private set => _currentWeapon = value;
    }
    private Weapon _currentWeapon;

    public LayerMask PlayerMask
    {
        get => _playerMask;
        private set => _playerMask = value;
    }
    [SerializeField] private LayerMask _playerMask;

    public LayerMask UnwalkableMask
    {
        get => _unwalkableMask;
        private set => _unwalkableMask = value;
    }
    [SerializeField] private LayerMask _unwalkableMask;

    public float EnemySightDistance //how far away from the target the enemy will first notice it
    {
        get => _enemySightDistance;
        private set => _enemySightDistance = value;
    }
    [SerializeField] private float _enemySightDistance;

    public float EnemyShootDistance //how far away from the target the enemy will shoot
    {
        get => _enemyShootDistance;
        private set => _enemyShootDistance = value;
    }
    [SerializeField] private float _enemyShootDistance;

    public PlayerMovement PlayerMovement
    {
        get => _playerMovement;
        private set => _playerMovement = value;
    }
    private PlayerMovement _playerMovement;

    public bool mustPathfind; //if true, the enemy must move. if false, the enemy stays still

    private EnemyState currentState;
    public EnemyStateHuntingPlayer stateHuntingPlayer = new EnemyStateHuntingPlayer();
    public EnemyStateRecallingBullets stateRecallingBullets = new EnemyStateRecallingBullets();

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        CurrentWeapon = GetComponentInChildren<Weapon>();
        target = player.transform;
        PlayerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        ChangeState(stateHuntingPlayer);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            StopCoroutine("FollowPath");
            targetIndex = 0;
            path = newPath;

            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            if (mustPathfind)
            {
                Rb.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            }
            yield return null;
        }
    }
}
