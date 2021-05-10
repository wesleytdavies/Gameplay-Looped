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

    private GameObject holder;
    private Weapon weapon;
    private SpriteRenderer weaponSpriteRenderer;

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        weaponSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        holder = weapon.holder;
        if (holder != null)
        {
            //mouse rotation code based on this thread: https://answers.unity.com/questions/10615/rotate-objectweapon-towards-mouse-cursor-2d.html
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane; //distance from screen to mouse
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePosition.x -= objectPosition.x;
            mousePosition.y -= objectPosition.y;
            RotationAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotationAngle));

            //put sprite either behind or in front of player
            if (RotationAngle >= 0 && RotationAngle <= 180)
            {
                weaponSpriteRenderer.sortingOrder = -1;
            }
            else
            {
                weaponSpriteRenderer.sortingOrder = 1;
            }
        }
    }

    private void LateUpdate()
    {
        if (holder != null)
        {
            transform.position = holder.transform.position;
        }
    }
}
