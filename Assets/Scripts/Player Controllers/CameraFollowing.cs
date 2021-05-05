using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float cameraDelay; //how long it takes the camera to catch up to its target
    [SerializeField] private bool followWeapon; //whether the camera should follow the direction of the player's held weapon

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraDelay * Time.deltaTime);
    }
}
