using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 targetPosition;
    [SerializeField] private float cameraCatchUpSpeed; //how quickly the camera to catch up to its target
    [SerializeField] private bool followCursor; //whether the camera should follow the direction of the player's cursor
    [SerializeField] private float cursorExtendLength; //how far out the camera should extend in the cursor direction

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (followCursor)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition -= new Vector2(transform.position.x, transform.position.y);
            mousePosition.Normalize();
            mousePosition *= cursorExtendLength;
            targetPosition = new Vector3(transform.position.x + mousePosition.x, transform.position.y + mousePosition.y, mainCamera.transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        }
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraCatchUpSpeed * Time.deltaTime);
        //mainCamera.transform.position = targetPosition;
    }
}
