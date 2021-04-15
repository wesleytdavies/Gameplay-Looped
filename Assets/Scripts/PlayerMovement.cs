using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool isMoving = false;
    private Vector2 moveDirection;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.zero;
    }

    void Update()
    {
        //directional movement
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            moveDirection.x = 1 / Mathf.Sqrt(2);
            moveDirection.y = 1 / Mathf.Sqrt(2);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            isMoving = true;

            moveDirection.x = -1 / Mathf.Sqrt(2);
            moveDirection.y = 1 / Mathf.Sqrt(2);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            isMoving = true;

            moveDirection.x = 1 / Mathf.Sqrt(2);
            moveDirection.y = -1 / Mathf.Sqrt(2);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            moveDirection.x = -1 / Mathf.Sqrt(2);
            moveDirection.y = -1 / Mathf.Sqrt(2);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            moveDirection.x = 0;
            moveDirection.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            moveDirection.x = 0;
            moveDirection.y = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            moveDirection.x = 1;
            moveDirection.y = 0;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            moveDirection.x = -1;
            moveDirection.y = 0;
        }
        else
        {
            isMoving = false;
            rb.velocity = new Vector2(0, 0);
        }
        if (isMoving)
        {
            rb.velocity = new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y);
        }
    }
}
