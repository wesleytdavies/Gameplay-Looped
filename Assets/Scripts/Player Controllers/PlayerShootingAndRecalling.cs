using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingAndRecalling : MonoBehaviour
{
    private static readonly float recallRadius = 0.75f; //how far away the bullet can be from the barrel to be recalled

    private Weapon currentWeapon;
    private GameObject currentWeaponObject;
    private Inventory inventory;
    private Camera mainCamera;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        currentWeaponObject = inventory.weaponInventory[0];
        currentWeapon = currentWeaponObject.GetComponent<Weapon>();
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon.BulletCount > 0)
            {
                if (!currentWeapon.IsCoolingDown)
                {
                    if (currentWeapon.HasExcessEnergy)
                    {
                        currentWeapon.Fire(true);
                    }
                    else
                    {
                        currentWeapon.Fire(false);
                    }
                }
            }
            else
            {
                //play click sound (out of ammo)
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

    IEnumerator CameraZoom()
    {
        yield return null;
    }
}
