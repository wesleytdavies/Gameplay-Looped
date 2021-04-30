using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Weapon currentWeapon;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletCount > 0)
            {
                float bulletAngle = Vector2.SignedAngle(transform.position, Input.mousePosition);
                FireBullet(bulletAngle);
            }
        }
    }

    void FireBullet(float angle)
    {

    }
}
