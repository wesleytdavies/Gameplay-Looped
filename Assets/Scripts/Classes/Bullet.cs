using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Properties
    public Vector2 ExitLocation //the location where the bullet is shot from
    {
        get
        {
            return _exitLocation;
        }
        private set
        {
            _exitLocation = value;
        }
    }
    private Vector2 _exitLocation;

    public Vector2 ForwardDirection //the direction the bullet moves in forward time
    {
        get
        {
            return _forwardDirection;
        }
        private set
        {
            _forwardDirection = value;
        }
    }
    private Vector2 _forwardDirection;

    public float Speed //how fast the bullet is moving
    {
        get
        {
            return _speed;
        }
        private set
        {
            _speed = value;
        }
    }
    private float _speed;
    #endregion

    void Start()
    {
        ExitLocation = transform.position;
    }

    void Update()
    {
        
    }
}
