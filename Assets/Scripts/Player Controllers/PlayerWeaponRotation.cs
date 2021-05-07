using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponRotation : MonoBehaviour
{
    public float RotationAngle
    {
        get => _rotationAngle;
        private set => _rotationAngle = value;
    }
    private float _rotationAngle;

    [SerializeField] private GameObject holder;

    void Start()
    {
        
    }

    void Update()
    {
        //mouse rotation code based on this thread: https://answers.unity.com/questions/10615/rotate-objectweapon-towards-mouse-cursor-2d.html
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; //distance from screen to mouse
        Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x -= objectPosition.x;
        mousePosition.y -= objectPosition.y;
        RotationAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationAngle));

        Vector2 flipScale; //if y is negative, this object gets flipped horizontally
        if (RotationAngle >= 90 || RotationAngle <= -90)
        {
            flipScale = transform.localScale;
            flipScale.y = -1;
            transform.localScale = flipScale;
        }
        else if (RotationAngle < 90 && RotationAngle > -90)
        {
            flipScale = transform.localScale;
            flipScale.y = 1;
            transform.localScale = flipScale;
        }
    }

    private void LateUpdate()
    {
        transform.position = holder.transform.position;
    }
}
