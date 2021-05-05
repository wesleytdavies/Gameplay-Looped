using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit to Sebastian Lague on YouTube for pathfinding tutorials
public class Unit : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float speed;
    Vector3[] path;
    int targetIndex;
    private Rigidbody2D rb;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask unwalkableMask;
    [SerializeField] private float enemySightDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PathRequestManager.RequestPath(rb.position, target.position, OnPathFound);
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
            rb.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }
    }
}
