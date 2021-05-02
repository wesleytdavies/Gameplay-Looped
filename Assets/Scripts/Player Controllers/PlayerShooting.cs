using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Weapon currentWeapon;
    public Pistol pistol = new Pistol(); //TODO: add the weapon component to the player instead

    void Start()
    {
        currentWeapon = pistol;
        //TODO: get rid of these commented lines, they're just for testing
        //currentWeapon.Initialize();
        //Debug.Log(currentWeapon.MagazineSize);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //if (bulletCount > 0)
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
