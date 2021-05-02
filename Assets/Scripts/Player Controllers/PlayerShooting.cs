using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Weapon currentWeapon;
    private GameObject currentWeaponObject;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    void Start()
    {
        currentWeaponObject = inventory.WeaponInventory[0];
        currentWeapon = currentWeaponObject.GetComponent<Weapon>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon.bulletCount > 0)
            {
                currentWeapon.Fire();
            }
        }
    }
}
