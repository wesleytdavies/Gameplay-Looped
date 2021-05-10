using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingAndRecalling : MonoBehaviour
{
    private static readonly float recallRadius = 0.75f; //how far away the bullet can be from the barrel to be recalled

    private float cameraBaseSize; //normal size of the camera
    private float cameraMinSize; //size of the camera at its most zoomed in
    private float zoomTime; //how long the camera has been zooming
    [SerializeField] private float zoomDuration; //how long the camera takes to zoom in/out in seconds. zoomDuration * 2 is the duration of CameraZoom()

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
        cameraBaseSize = mainCamera.orthographicSize;
        cameraMinSize = cameraBaseSize - 1;
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
                        StartCoroutine(CameraZoom());
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
                        if (nearbyObjects[i].GetComponent<LoopableObject>().originator == currentWeaponObject)
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
        zoomTime = 0f;
        while (zoomTime <= zoomDuration)
        {
            zoomTime += Time.deltaTime;
            mainCamera.orthographicSize = Mathf.Lerp(cameraBaseSize, cameraMinSize, zoomTime / zoomDuration);
            yield return null;
        }
        while (zoomTime > zoomDuration && zoomTime <= zoomDuration * 2)
        {
            zoomTime += Time.deltaTime;
            mainCamera.orthographicSize = Mathf.Lerp(cameraMinSize, cameraBaseSize, zoomTime / (zoomDuration * 2));
            yield return null;
        }
        if (zoomTime > zoomDuration * 2)
        {
            yield break;
        }
    }
}
