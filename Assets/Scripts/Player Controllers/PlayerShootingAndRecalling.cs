using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingAndRecalling : MonoBehaviour
{
    private static readonly float recallRadius = 1f; //how far away the bullet can be from the barrel to be recalled

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
        if (Input.GetButtonDown("Fire2"))
        {
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(currentWeapon.Barrel.position, recallRadius);
            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                if (nearbyObjects[i].GetComponent<LoopableObject>() != null)
                {
                    if (nearbyObjects[i].GetComponent<LoopableObject>().IsReversing)
                    {
                        if (nearbyObjects[i].GetComponent<LoopableObject>().originator = currentWeaponObject)
                        {
                            currentWeapon.Recall(nearbyObjects[i].gameObject);
                            return; //prevents recalling multiple bullets at once
                        }
                    }
                }
            }
        }
    }
}
