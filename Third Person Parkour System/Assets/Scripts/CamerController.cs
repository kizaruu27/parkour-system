using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float distance = 5;

    [SerializeField] private Vector2 frammingOffset;

    [SerializeField] private float minVerticalAngle = -45;
    [SerializeField] private float maxVerticalAngle = 45;

    [SerializeField] private float rotationSpeed = 2;

    [SerializeField] private bool invertedX;
    [SerializeField] private bool invertedY;

    private float invertedXVal;
    private float invertedYVal;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        invertedXVal = invertedX ? -1 : 1;
        invertedYVal = invertedY ? -1 : 1;
        
        rotationX += Input.GetAxisRaw("Mouse Y") * invertedYVal * rotationSpeed;
        rotationY += Input.GetAxisRaw("Mouse X") * invertedXVal * rotationSpeed;

        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 focusPosition = followTarget.position + new Vector3(frammingOffset.x, frammingOffset.y);
        
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }
    
    public Quaternion PlannarRotation => Quaternion.Euler(0, rotationY, 0);
}
























